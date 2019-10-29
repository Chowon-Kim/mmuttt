using Samsung.AppCommon.Logger;
using Samsung.SmartSearchApp.Search.SearchEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Samsung.SmartSearchApp.View.Controls
{
    public class HighlightTextBlock : TextBlock
    {
        private const int FULL_LENGTH = 150;
        private const int HEAD_TAIL_LENGTH = 20;
        private const int MAX_HIT_COUNT = 3;

        #region [Dependency Properties]
        #region [Keyword]
        public static DependencyProperty KeywordProperty = 
            DependencyProperty.Register("Keyword", typeof(string), typeof(HighlightTextBlock), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnKeywordChanged)));

        public string Keyword
        {
            get { return (string)GetValue(KeywordProperty); }
            set { SetValue(KeywordProperty, value); }
        }

        private static void OnKeywordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HighlightTextBlock htb = d as HighlightTextBlock;
            if (htb != null && string.IsNullOrEmpty(e.NewValue as string)) // 검색 키워드가 지워졌을 경우, 원래 text로 세팅
            {
                htb.Inlines.Clear();
                htb.SetColorOfString(htb.FullText);
                return;
            }

            if (htb.IsEnableMultiHighlight && htb.KeywordList != null && htb.KeywordList.Count > 0)
                htb.SearchMultipleKeywordAndHighlight(htb.KeywordList);
            else
                htb.SearchKeywordAndHighlight((string)e.NewValue);
        }
        #endregion

        #region [KeywordList]
        public static DependencyProperty KeywordListProperty =
            DependencyProperty.Register("KeywordList", typeof(ObservableCollection<string>), typeof(HighlightTextBlock),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnKeywordListChanged)));

        public ObservableCollection<string> KeywordList
        {
            get { return (ObservableCollection<string>)GetValue(KeywordListProperty); }
            set { SetValue(KeywordListProperty, value); }
        }

        private static void OnKeywordListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HighlightTextBlock htb = d as HighlightTextBlock;
            if (htb != null)
            {
                if (!htb.IsEnableMultiHighlight) return;

                if ((e.NewValue == null) || (e.NewValue as ObservableCollection<string>).Count == 0) // 검색 키워드가 지워졌을 경우, 원래 text로 세팅
                {
                    htb.Inlines.Clear();
                    htb.SetColorOfString(htb.FullText);
                    return;
                }

                if (e.NewValue != null && e.NewValue is ObservableCollection<string>)
                    htb.SearchMultipleKeywordAndHighlight(e.NewValue as ObservableCollection<string>);
            }
        }
        #endregion

        #region [FullText]
        public static DependencyProperty FullTextProperty =
            DependencyProperty.Register("FullText", typeof(string), typeof(HighlightTextBlock),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnFullTextChanged)));

        public string FullText
        {
            get { return (string)GetValue(FullTextProperty); }
            set { SetValue(FullTextProperty, value); }
        }

        private static void OnFullTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewValue as string)) return;

            HighlightTextBlock htb = d as HighlightTextBlock;
            if (htb != null)
            {
                htb.Inlines.Clear();
                htb.SetColorOfString(e.NewValue as string);
            }

            if (htb.IsEnableMultiHighlight && htb.KeywordList != null && htb.KeywordList.Count > 0)
                htb.SearchMultipleKeywordAndHighlight(htb.KeywordList);
            else if (string.IsNullOrEmpty(htb.Keyword) == false)
                htb.SearchKeywordAndHighlight(htb.Keyword);
        }
        #endregion

        #region [IsHighlightAllHits]
        public static DependencyProperty IsHighlightAllHitsProperty =
            DependencyProperty.Register("IsHighlightAllHits", typeof(bool), typeof(HighlightTextBlock), new PropertyMetadata(false));
        public bool IsHighlightAllHits
        {
            get { return (bool)GetValue(IsHighlightAllHitsProperty); }
            set { SetValue(IsHighlightAllHitsProperty, value); }
        }
        #endregion

        #region [HighlightForeground]
        public static DependencyProperty HighlightForegroundProperty =
            DependencyProperty.Register("HighlightForeground", typeof(SolidColorBrush), typeof(HighlightTextBlock),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black), FrameworkPropertyMetadataOptions.AffectsRender));

        public SolidColorBrush HighlightForeground
        {
            get { return (SolidColorBrush)GetValue(HighlightForegroundProperty); }
            set { SetValue(HighlightForegroundProperty, value); }
        }
        #endregion

        #region [HighlightBackground]
        public static DependencyProperty HighlightBackgroundProperty =
            DependencyProperty.Register("HighlightBackground", typeof(SolidColorBrush), typeof(HighlightTextBlock),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Transparent), FrameworkPropertyMetadataOptions.AffectsRender));

        public SolidColorBrush HighlightBackground
        {
            get { return (SolidColorBrush)GetValue(HighlightBackgroundProperty); }
            set { SetValue(HighlightBackgroundProperty, value); }
        }
        #endregion

        #region [IsCaseSensitive]
        public static DependencyProperty IsCaseSensitiveProperty =
            DependencyProperty.Register("IsCaseSensitive", typeof(bool), typeof(HighlightTextBlock), new PropertyMetadata(false));
        public bool IsCaseSensitive
        {
            get { return (bool)GetValue(IsCaseSensitiveProperty); }
            set { SetValue(IsCaseSensitiveProperty, value); }
        }
        #endregion

        #region [DefaultRunStyle]
        public static DependencyProperty DefaultRunStyleProperty =
            DependencyProperty.Register("DefaultRunStyle", typeof(Style), typeof(HighlightTextBlock), new PropertyMetadata(null));
        public Style DefaultRunStyle
        {
            get { return (Style)GetValue(DefaultRunStyleProperty); }
            set { SetValue(DefaultRunStyleProperty, value); }
        }
        #endregion

        #region [IsEnableMultiHighlight]
        public static DependencyProperty IsEnableMultiHighlightProperty =
            DependencyProperty.Register("IsEnableMultiHighlight", typeof(bool), typeof(HighlightTextBlock), new PropertyMetadata(false));
        public bool IsEnableMultiHighlight
        {
            get { return (bool)GetValue(IsEnableMultiHighlightProperty); }
            set { SetValue(IsEnableMultiHighlightProperty, value); }
        }
        #endregion

        #region [IsEnableTailor]
        public static DependencyProperty IsEnableTailorProperty =
            DependencyProperty.Register("IsEnableTailor", typeof(bool), typeof(HighlightTextBlock), new PropertyMetadata(false));
        public bool IsEnableTailor
        {
            get { return (bool)GetValue(IsEnableTailorProperty); }
            set { SetValue(IsEnableTailorProperty, value); }
        }
        #endregion

        // 키워드 앞뒤 string 길이를 제한하기 위한 property
        #region [TailorLength]
        public static DependencyProperty TailorLengthProperty =
            DependencyProperty.Register("TailorLength", typeof(int), typeof(HighlightTextBlock), new PropertyMetadata(HEAD_TAIL_LENGTH));
        public int TailorLength
        {
            get { return (int)GetValue(TailorLengthProperty); }
            set { SetValue(TailorLengthProperty, value); }
        }
        #endregion

        // 키워드를 포함한 전체 string 길이를 제한하기 위한 property
        #region [FullLength]
        public static DependencyProperty FullLengthProperty =
            DependencyProperty.Register("FullLength", typeof(int), typeof(HighlightTextBlock), new PropertyMetadata(FULL_LENGTH));
        public int FullLength
        {
            get { return (int)GetValue(FullLengthProperty); }
            set { SetValue(FullLengthProperty, value); }
        }
        #endregion
        #endregion

        public HighlightTextBlock()
        {
            //this.Inlines.Clear();
            this.Background = new SolidColorBrush(System.Windows.Media.Colors.Transparent);
        }

        public void SearchKeywordAndHighlight(string keyword)
        {
            this.Inlines.Clear();
            if (string.IsNullOrEmpty(FullText)) return;

            if (IsHighlightAllHits)
                HighlightHits(keyword);
            else
                HighlightOnlyOneHit(keyword);
        }

        public void SearchMultipleKeywordAndHighlight(ObservableCollection<string> keywordList)
        {
            this.Inlines.Clear();
            if (string.IsNullOrEmpty(FullText)) return;

            HighlightHits(keywordList);
        }

        // [Not used now] Highlight 3 matched keywords...
        public void HighlightHits(string originalKeyword)
        {
            if (string.IsNullOrEmpty(FullText)) return;

            // default로는 대소문자 구분없이 highlight 시킴
            string oriFullText = FullText;
            string tempFullText = IsCaseSensitive ? FullText : FullText.ToLower();
            string tempKeyword = IsCaseSensitive ? originalKeyword : originalKeyword.ToLower();

            try
            {
                if (tempFullText.Contains(tempKeyword))
                {
                    string pre = string.Empty;
                    string post = string.Empty;

                    //Logger.LogMessage(string.Format("{0} 파일 : {1} 문장 내에 포함되어 있음.", Path.GetFileName(fullPath), text));
                    string replaced = tempFullText.Replace('\n', ' '); // 임시로 줄넘김 문자를 띄어쓰기 문자로 대체시켜서 보여줌
                                                                       //replaced = Regex.Replace(replaced, @"[\s]", "", RegexOptions.None);
                    for (int i = 0; i < MAX_HIT_COUNT; i++)
                    {
                        if (string.IsNullOrEmpty(replaced) || replaced.Contains(tempKeyword) == false) break;

                        // 검색하는 키워드 앞뒤로 얼만큼 자를지 정해야 할 듯
                        int keyIndex = replaced.IndexOf(tempKeyword);
                        if (keyIndex > 0)
                        {
                            if (IsEnableTailor && keyIndex > TailorLength)
                                pre = "..." + oriFullText.Substring(keyIndex - TailorLength, TailorLength);
                            else
                                pre = oriFullText.Substring(0, keyIndex);
                        }

                        string matchedText = oriFullText.Substring(keyIndex, tempKeyword.Length);
                        SetColorOfString(pre, matchedText, ""); // 화면에 보여지는 text는 original text로부터 추출

                        int postKeyIndex = keyIndex + tempKeyword.Length;
                        if (IsEnableTailor && postKeyIndex < replaced.Length)
                        {
                            replaced = replaced.Substring(postKeyIndex);
                            oriFullText = oriFullText.Substring(postKeyIndex);
                        }
                        else
                            replaced = oriFullText = string.Empty;
                    }

                    // MAX_HIT_COUNT 까지 돌고 난 뒤 남는 문구에 대한 처리
                    if (IsEnableTailor && replaced.Length > TailorLength)
                        oriFullText = oriFullText.Substring(0, TailorLength) + "...";

                    SetColorOfString("", "", oriFullText); // 화면에 보여지는 text는 original text로부터 추출
                }
                else
                    SetColorOfString(FullText); // 화면에 보여지는 text는 original text로부터 추출
            }
            catch (Exception ex)
            {
                Logger.LogMessage(string.Format("Failed to highlight couple of keywords. ({0})", ex.Message), System.Diagnostics.TraceLevel.Error);
            }
        }

        private List<string> FindMatchedKeywordFromSource(List<string> operatorWords)
        {
            List<string> matchedString = new List<string>();

            if (string.IsNullOrEmpty(FullText))
                return matchedString;

            if (operatorWords.Count > 0)
            {
                //if (!IsCaseSensitive)
                {
                    matchedString = operatorWords.Where(x => (FullText.ToLower().Contains(x.ToLower()))).ToList();
                }
                //else
                //{
                //    foreach (string op in operatorWords)
                //    {
                //        if (FullText.Contains(op))
                //            matchedString.Add(op);
                //    }
                //}
            }

            return matchedString;
        }

        // [IN] match되는 걸 확인한 word만 저장된 keyword List
        public void HighlightHits(ObservableCollection<string> originalKeywordList)
        {
            if (string.IsNullOrEmpty(FullText)) return;
            
            // default로는 대소문자 구분없이 highlight 시킴
            string oriFullText = FullText;
            //string tempFullText = IsCaseSensitive ? FullText : FullText.ToLower();
            string tempFullText = FullText.ToLower();

            //string replaced = tempFullText.Replace('\n', ' '); // 임시로 줄넘김 문자를 띄어쓰기 문자로 대체시켜서 보여줌
            tempFullText = tempFullText.Replace('\n', ' ');
            if (string.IsNullOrEmpty(tempFullText)) return;

            try
            {
                List<string> matchedKeywordList = FindMatchedKeywordFromSource(originalKeywordList.ToList());

                List<KeywordMatchInfo> keyInfoList = new List<KeywordMatchInfo>();
                foreach (string matchedKeyword in matchedKeywordList)
                {
                    //string tempKeyword = IsCaseSensitive ? matchedKeyword : matchedKeyword.ToLower();
                    string tempKeyword = matchedKeyword.ToLower();
                    int keyIndex = tempFullText.IndexOf(tempKeyword);
                    string matchedString = oriFullText.Substring(keyIndex, tempKeyword.Length); // match되는 string을 도려내서 저장
                    KeywordMatchInfo info = new KeywordMatchInfo(matchedString, keyIndex, matchedString.Length);
                    keyInfoList.Add(info);
                }

                if (keyInfoList.Count > 0)
                {
                    keyInfoList.Sort((x, y) => x.MatchedIndex.CompareTo(y.MatchedIndex));

                    int startIndex = 0;
                    string sum = string.Empty;

                    foreach (KeywordMatchInfo info in keyInfoList)
                    {
                        string pre = string.Empty;
                        string keyword = string.Empty;
                        string post = string.Empty;

                        if (IsEnableTailor && startIndex == 0 && info.MatchedIndex > 0)
                        {
                            SetColorOfString("...", "", "");
                            startIndex = info.MatchedIndex;
                        }

                        if (IsEnableTailor && sum.Length > FullLength) break;

                        if (startIndex > info.MatchedIndex) continue;

                        pre = oriFullText.Substring(startIndex, info.MatchedIndex - startIndex);
                        if (pre.Length + sum.Length > FullLength)
                        {
                            // 키워드 사이의 string 길이가 길 경우 잘라서 붙여줌.
                            pre = pre.Substring(0, (FullLength - sum.Length));
                            SetColorOfString(pre);
                        }
                        else
                        {
                            keyword = info.Keyword;
                            SetColorOfString(pre, keyword);
                        }

                        sum = sum + pre + keyword;
                        startIndex = info.MatchedIndex + info.Keyword.Length;
                    }

                    // 아직 뒤에 남은 문구가 있을 때
                    if (startIndex < oriFullText.Length)
                    {
                        if (IsEnableTailor)
                        {
                            if (sum.Length < FullLength)
                            {
                                string post = oriFullText.Substring(startIndex);

                                // 남은 문구를 잘라서 붙이기 위한 작업
                                int bufferLeng = FullLength - sum.Length;
                                if (post.Length > bufferLeng)
                                    post = post.Substring(0, bufferLeng);

                                SetColorOfString("", "", post);
                            }
                            else
                            {
                                // FullLength가 넘으면 ...만 붙임
                                SetColorOfString("", "", "...");
                            }
                        }
                        else
                        {
                            string post = oriFullText.Substring(startIndex);
                            SetColorOfString("", "", post);
                        }
                    }
                }
                else
                    SetColorOfString(FullText);
            }
            catch (Exception ex)
            {
                Logger.LogMessage(string.Format("Failed to highlight multiple keywords. ({0})", ex.Message), System.Diagnostics.TraceLevel.Error);
            }
        }

        // 매치되는 키워드를 하나만 찾아서 Highlight 시키는 함수
        public void HighlightOnlyOneHit(string originalKeyword)
        {
            if (string.IsNullOrEmpty(FullText)) return;

            try
            {
                // default로는 대소문자 구분없이 highlight 시킴
                string oriFullText = FullText;
                string tempFullText = IsCaseSensitive ? FullText : FullText.ToLower();
                string tempKeyword = IsCaseSensitive ? originalKeyword : originalKeyword.ToLower();

                if (tempFullText.Contains(tempKeyword))
                {
                    string pre = string.Empty;
                    string post = string.Empty;

                    //Logger.LogMessage(string.Format("{0} 파일 : {1} 문장 내에 포함되어 있음.", Path.GetFileName(fullPath), text));
                    string replaced = tempFullText.Replace('\n', ' '); // 임시로 줄넘김 문자를 띄어쓰기 문자로 대체시켜서 보여줌???
                                                                       //replaced = Regex.Replace(replaced, @"[\s]", "", RegexOptions.None);

                    int keyIndex = replaced.IndexOf(tempKeyword);
                    if (keyIndex > 0)
                    {
                        if (IsEnableTailor && keyIndex > TailorLength)
                            pre = "..." + oriFullText.Substring(keyIndex - TailorLength, TailorLength);
                        else
                            pre = oriFullText.Substring(0, keyIndex);
                    }

                    int postKeyIndex = keyIndex + tempKeyword.Length;
                    if (IsEnableTailor && (postKeyIndex + TailorLength < replaced.Length))
                        post = oriFullText.Substring(postKeyIndex, TailorLength) + "...";
                    else
                        post = oriFullText.Substring(postKeyIndex);

                    string matchedText = oriFullText.Substring(keyIndex, tempKeyword.Length);
                    SetColorOfString(pre, matchedText, post); // 화면에 보여지는 text는 original text로부터 추출
                }
                else
                    SetColorOfString(FullText); // 화면에 보여지는 text는 original text로부터 추출
            }
            catch (Exception ex)
            {
                Logger.LogMessage(string.Format("Failed to highlight one keyword. ({0})", ex.Message), System.Diagnostics.TraceLevel.Error);
            }
        }

        private void SetColorOfString(string pre="", string keyword="", string post="")
        {
            // Normal text
            if (string.IsNullOrEmpty(pre) == false)
            {
                var newRun = new Run(pre);
                newRun.Style = DefaultRunStyle;
                this.Inlines.Add(newRun);
            }

            // Highlighted text
            if (string.IsNullOrEmpty(keyword) == false) 
            {
                var newRun = new Run(keyword);
                newRun.Style = DefaultRunStyle;
                newRun.Foreground = HighlightForeground;
                newRun.Background = HighlightBackground;
                this.Inlines.Add(newRun);
            }
            
            // Normal text
            if (string.IsNullOrEmpty(post) == false)
            {
                var newRun = new Run(post);
                newRun.Style = DefaultRunStyle;
                this.Inlines.Add(newRun);
            }
        }
    }
}
