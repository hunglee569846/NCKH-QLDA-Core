using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace NCKH.Infrastruture.Binding.Extensions
{
    public static class StringExtension
    {
        public static string StripVietnameseChars(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            input = input.Trim();
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                "đ",
                "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                "í","ì","ỉ","ĩ","ị",
                "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                "d",
                "e","e","e","e","e","e","e","e","e","e","e",
                "i","i","i","i","i",
                "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                "u","u","u","u","u","u","u","u","u","u","u",
                "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                input = input.Replace(arr1[i], arr2[i]);
                input = input.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return input;
        }

        /// <summary>
        /// Loại bỏ các ký tự tiếng việt
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <returns>Chuỗi đã được loại bỏ các ký tự tiếng việt</returns>
        public static string StripVietnameseChars1(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            input = input.Trim();
            const string sUtf8Lower = "a|á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ|đ|e|é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ|i|í|ì|ỉ|ĩ|ị|o|ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ|u|ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự|y|ý|ỳ|ỷ|ỹ|ỵ";

            const string sUtf8Upper = "A|Á|À|Ả|Ã|Ạ|Ă|Ắ|Ằ|Ẳ|Ẵ|Ặ|Â|Ấ|Ầ|Ẩ|Ẫ|Ậ|Đ|E|É|È|Ẻ|Ẽ|Ẹ|Ê|Ế|Ề|Ể|Ễ|Ệ|I|Í|Ì|Ỉ|Ĩ|Ị|O|Ó|Ò|Ỏ|Õ|Ọ|Ô|Ố|Ồ|Ổ|Ỗ|Ộ|Ơ|Ớ|Ờ|Ở|Ỡ|Ợ|U|Ú|Ù|Ủ|Ũ|Ụ|Ư|Ứ|Ừ|Ử|Ữ|Ự|Y|Ý|Ỳ|Ỷ|Ỹ|Ỵ";

            const string sUcs2Lower = "a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|d|e|e|e|e|e|e|e|e|e|e|e|e|i|i|i|i|i|i|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|u|u|u|u|u|u|u|u|u|u|u|u|y|y|y|y|y|y";

            const string sUcs2Upper = "A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|D|E|E|E|E|E|E|E|E|E|E|E|E|I|I|I|I|I|I|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|U|U|U|U|U|U|U|U|U|U|U|U|Y|Y|Y|Y|Y|Y";

            var aUtf8Lower = sUtf8Lower.Split('|');

            var aUtf8Upper = sUtf8Upper.Split('|');

            var aUcs2Lower = sUcs2Lower.Split('|');

            var aUcs2Upper = sUcs2Upper.Split('|');

            int nLimitChar = aUtf8Lower.GetUpperBound(0);

            for (int i = 1; i <= nLimitChar; i++)
            {
                input = input.Replace(aUtf8Lower[i], aUcs2Lower[i]);

                input = input.Replace(aUtf8Upper[i], aUcs2Upper[i]);
            }
            const string sUcs2Regex = @"[A-Za-z0-9- ]";
            var sEscaped =
                new Regex(sUcs2Regex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture).
                    Replace(input, String.Empty);
            if (String.IsNullOrEmpty(sEscaped))
            {
                return input;
            }
            sEscaped = sEscaped.Replace("[", "\\[");
            sEscaped = sEscaped.Replace("]", "\\]");
            sEscaped = sEscaped.Replace("^", "\\^");
            string sEscapedregex = @"[" + sEscaped + "]";
            return
                new Regex(
                    sEscapedregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture).
                    Replace(input, String.Empty);
        }

        /// <summary>
        /// Đọc số thành chữ tiếng việt
        /// </summary>
        /// <param name="number">Chuỗi đầu vào.</param>
        /// <returns>Chữ tiếng việt</returns>
        public static string Number2Text(this int number)
        {
            if (number < 0)
                return "âm " + Number2Text(Math.Abs(number)).ToLower();
            int mLen, mDigit;
            string mTemp = "";
            string[] mNumText;
            //Xóa các dấu "," nếu có
            string sNumber = Convert.ToString(number);
            sNumber = sNumber.Replace(",", "");
            mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');
            mLen = sNumber.Length - 1; // trừ 1 vì thứ tự đi từ 0
            for (int i = 0; i <= mLen; i++)
            {
                mDigit = Convert.ToInt32(sNumber.Substring(i, 1));
                mTemp = mTemp + " " + mNumText[mDigit];
                if (mLen == i) break;  // Chữ số cuối cùng không cần xét tiếp 
                switch ((mLen - i) % 9)
                {
                    case 0:
                        mTemp += " tỷ";
                        if (sNumber.Substring(i + 1, 3) == "000") i += 3;
                        if (sNumber.Substring(i + 1, 3) == "000") i += 3;
                        if (sNumber.Substring(i + 1, 3) == "000") i += 3;
                        break;
                    case 6:
                        mTemp += " triệu";
                        if (sNumber.Substring(i + 1, 3) == "000") i += 3;
                        if (sNumber.Substring(i + 1, 3) == "000") i += 3;
                        break;
                    case 3:
                        mTemp += " nghìn";
                        if (sNumber.Substring(i + 1, 3) == "000") i += 3;
                        break;
                    default:
                        switch ((mLen - i) % 3)
                        {
                            case 2:
                                mTemp += " trăm";
                                break;
                            case 1:
                                mTemp += " mươi";
                                break;
                        }
                        break;
                }
            }
            //Loại bỏ trường hợp x00
            mTemp = mTemp.Replace("không mươi không", "");
            //Loại bỏ trường hợp 00x 
            mTemp = mTemp.Replace("không mươi ", "linh ");
            //Loại bỏ trường hợp x0, x>=2
            mTemp = mTemp.Replace("mươi không", "mươi");
            //Fix trường hợp 10
            mTemp = mTemp.Replace("một mươi", "mười");
            //Fix trường hợp x4, x>=2
            mTemp = mTemp.Replace("mươi bốn", "mươi tư");
            //Fix trường hợp x04
            mTemp = mTemp.Replace("linh bốn", "linh tư");
            //Fix trường hợp x5, x>=2
            mTemp = mTemp.Replace("mươi năm", "mươi lăm");
            //Fix trường hợp x1, x>=2
            mTemp = mTemp.Replace("mươi một", "mươi mốt");
            //Fix trường hợp x15
            mTemp = mTemp.Replace("mười năm", "mười lăm");
            //Bỏ ký tự space
            mTemp = mTemp.Trim();
            //Viết hoa ký tự đầu tiên
            mTemp = mTemp.Substring(0, 1).ToUpper() + mTemp.Substring(1, mTemp.Length - 1);
            return mTemp;
        }

        /// <summary>
        /// Đọc số thành chữ tiếng anh
        /// </summary>
        /// <param name="number">Chuỗi đầu vào.</param>
        /// <returns>Chữ tiếng anh</returns>
        public static string NumberToWords(this int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        /// <summary>
        /// Get the first character every word in string
        /// Ex: Tran Van Manh =>TVM
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetFirstCharacters(string value)
        {
            string firstCharacters = string.Empty;
            if (string.IsNullOrWhiteSpace(value))
            {
                return firstCharacters;
            }

            string[] stringArray = value.Split(' ');
            firstCharacters = stringArray.Aggregate(firstCharacters, (current, item) => current + item[0]);

            return firstCharacters;
        }

        /// <summary>
        /// Lấy 2 ký tự đầu của họ tên
        /// Ex: Tran Van Manh =>TM
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <returns>Chuỗi đã được chuẩn hóa</returns>
        public static string GetFirstAndLastLetterFromName(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return "";

            string name = input.Trim();
            string first = name.Substring(0, 1);
            char[] charArray = name.ToCharArray();
            Array.Reverse(charArray);
            string last = new string(charArray);

            if (last.Contains(' '))
            {
                char[] splitchar = { ' ' };
                string[] temp = last.Split(splitchar);
                int c = temp[0].ToString().Length - 1;
                last = temp[0].ToString().Substring(c, 1);
            }
            else
            {
                return first.ToUpper();
            }

            return first.ToUpper() + last.ToUpper();
        }

        /// <summary>
        /// Get first character in string
        /// Ex: Tran Van Manh => T
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string GetFirstCharacter(string value)
        {
            string firstCharacter = string.Empty;
            if (string.IsNullOrWhiteSpace(value))
            {
                return firstCharacter;
            }

            return value[0].ToString();
        }

        /// <summary>
        /// Viết hoa chữ cái đầu tiên và bỏ đi dấu space
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <returns>Chuỗi đã được chuẩn hóa</returns>
        public static string UpperCase(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;

            string result = "";
            //lấy danh sách các từ  
            string[] words = input.Split(' ');
            foreach (string word in words)
            {
                // từ nào là các khoảng trắng thừa thì bỏ  
                if (word.Trim() != "")
                {
                    if (word.Length > 1)
                        result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                    else
                        result += word.ToUpper() + " ";
                }

            }
            return result.Trim();
        }

        /// <summary>
        /// Viết hoa chữ cái đầu tiên và giữ nguyên vị trí
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <returns>Chuỗi đã được chuẩn hóa</returns>
        public static string UpperCaseSpace(this string input)
        {
            string result = input;
            if (!string.IsNullOrEmpty(input))
            {
                var words = input.Split(' ');
                for (int index = 0; index < words.Length; index++)
                {
                    var s = words[index];
                    if (s.Length > 0)
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }
                }
                result = string.Join(" ", words);
            }
            return result.Trim();
        }

        /// <summary>
        /// UpperCaseFirst in string
        /// Ex: tran van manh. ghm => Tran van manh. Ghm
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string UpperCaseFirst(this string input)
        {
            input = input.ToLower();
            var result = new StringBuilder(input.Length);
            if (!string.IsNullOrEmpty(input))
            {
                bool IsNewSentense = true;
                for (int i = 0; i < input.Length; i++)
                {
                    if (IsNewSentense && char.IsLetter(input[i]))
                    {
                        result.Append(char.ToUpper(input[i]));
                        IsNewSentense = false;
                    }
                    else
                        result.Append(input[i]);

                    if (input[i] == '!' || input[i] == '?' || input[i] == '.')
                    {
                        IsNewSentense = true;
                    }
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Get some first words string string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetWords(string value, int limit)
        {
            string words = string.Empty;

            if (value != null)
            {
                string[] stringArray = value.Split(' ');

                if (limit > stringArray.Length)
                    return "limit can not bigger amout word in string";

                if (limit == 0 || limit == stringArray.Length)
                    return value;

                for (int i = 0; i < limit; i++)
                {
                    words += stringArray[i] + " ";
                }

                return words.Remove(words.Length - 1);
            }
            return words;
        }

        /// <summary>
        /// Find word in string
        /// return startIndex,endIndex if the word exists in string otherwise it returns to -1
        /// Example: value is "Tran Van Manh", wordToFind is "Tran" => 0,3
        /// </summary>
        /// <param name="value"></param>
        /// <param name="wordToFind"></param>
        /// <returns>startIndex,endIndex / -1</returns>
        public static string FindWord(string value, string wordToFind)
        {
            if (value == null)
            {
                throw new Exception("Value cannot null");
            }
            if (wordToFind == null)
            {
                throw new Exception("Word to find cannot null");
            }

            int startIndex = value.IndexOf(wordToFind, StringComparison.Ordinal);

            if (startIndex >= 0)
            {
                return startIndex + "," + (startIndex + (wordToFind.Length - 1));
            }

            return "-1";
        }


        /// <summary>
        /// Loại bỏ các ký tự phân cách
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <returns>Chuỗi đã được lại bỏ các ký tự phân cách</returns>
        public static string StripDelimiters(this string input)
        {
            var delimiters = new[] { '|', '"', '\'', ';', ',', '.' };
            return input.StripChars(delimiters);
        }

        /// <summary>
        /// Loại bỏ các ký tự xác định.
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <param name="strips">Mảng các ký tự cần loại bỏ.</param>
        /// <returns></returns>
        public static string StripChars(this string input, params char[] strips)
        {
            if (strips == null)
            {
                throw new ArgumentNullException("strips");
            }

            var scanner = new StringBuilder(input);
            var builder = new StringBuilder(scanner.Length);
            for (var i = 0; i < scanner.Length; i++)
            {
                if (strips.Any(c => scanner[i].Equals(c)))
                {
                    continue;
                }
                builder.Append(scanner[i]);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Thay thế các nhóm ký tự bằng một nhóm ký tự khác
        /// </summary>
        /// var a = new[] { "a", "b"};
        /// var b = new[] { 'c', 'd' };
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <param name="strips">Mảng các chữ cần loại bỏ.</param>
        /// <param name="replacements">Mảng các chữ cần thay thế.</param>
        /// <returns></returns>
        public static string ReplaceCharGroups(this string input, string[] strips, char[] replacements)
        {
            if (strips == null)
            {
                throw new ArgumentNullException("strips");
            }
            if (replacements == null)
            {
                throw new ArgumentNullException("replacements");
            }
            if (strips.Length > replacements.Length)
            {
                throw new ArgumentException("Length of replacement array must be larger than strip array");
            }

            var scanner = new StringBuilder(input);
            for (var i = 0; i < scanner.Length; i++)
            {
                for (var j = 0; j < strips.Length; j++)
                {
                    if (strips[j].IndexOf(scanner[i]) != -1)
                    {
                        scanner[i] = replacements[j];
                    }
                }

            }
            return scanner.ToString();
        }

        /// <summary>
        /// Thay thế các nhóm ký tự bằng một nhóm ký tự khác
        /// </summary>
        /// var a = new[] { "a", "b"};
        ///  var b = new[] { "AA", "BB" };           
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <param name="strips">Mảng các chữ cần loại bỏ.</param>
        /// <param name="replacements">Mảng các chữ cần thay thế.</param>
        /// <returns></returns>
        public static string ReplaceCharGroups(this string input, string[] strips, string[] replacements)
        {
            if (strips == null)
            {
                throw new ArgumentNullException("strips");
            }
            if (replacements == null)
            {
                throw new ArgumentNullException("replacements");
            }
            if (strips.Length > replacements.Length)
            {
                throw new ArgumentException("Length of replacement array must be larger than strip array");
            }

            var scanner = input.Select(i => i.ToString(CultureInfo.InvariantCulture)).ToArray();
            for (var i = 0; i < scanner.Length; i++)
            {
                for (var j = 0; j < strips.Length; j++)
                {
                    if (strips[j].IndexOf(scanner[i], StringComparison.Ordinal) != -1)
                    {
                        scanner[i] = replacements[j];
                    }
                }

            }
            return string.Join("", scanner);
        }

        /// <summary>
        /// Remove html tag in string
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string RemoveHtml(this string value)
        {
            try
            {
                return Regex.Replace(value, @"<[^>]*>|&nbsp;", string.Empty);
            }
            catch
            {
                return value;
            }
        }

        /// <summary>
        /// Loại bỏ các ký tự html
        /// </summary>
        /// <param name="source">Chuỗi</param>
        /// <returns>Chuỗi đã được loại bỏ ký tự html</returns>
        public static string StripHtml(this string source)
        {
            try
            {
                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                var result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         RegexOptions.IgnoreCase);
                //result = Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         RegexOptions.IgnoreCase);

                // replace special characters:
                result = Regex.Replace(result,
                         @" ", " ",
                         RegexOptions.IgnoreCase);

                result = Regex.Replace(result,
                         @"&bull;", " * ",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&lsaquo;", "<",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&rsaquo;", ">",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&trade;", "(tm)",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&frasl;", "/",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&lt;", "<",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&gt;", ">",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&copy;", "(c)",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&reg;", "(r)",
                         RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         RegexOptions.IgnoreCase);

                // for testing
                //Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                var breaks = "\r\r\r";
                // Initial replacement target string for tabs
                var tabs = "\t\t\t\t\t";
                for (var index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks += breaks + "\r";
                    tabs += tabs + "\t";
                }

                return result.Trim();
            }
            catch
            {
                return source;
            }
        }

        /// <summary>
        /// Kiểm tra chuỗi có phải là địa chỉ email hay không
        /// </summary>
        /// <param name="input">Chuỗi</param>
        /// <returns></returns>
        public static bool IsEmailAddress(this string input)
        {
            var regex = new Regex("^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
        }


        /// <summary>
        /// Kiểm tra chuỗi có phải là địa chỉ số điện thoại hay không
        /// </summary>
        /// <param name="input">Chuỗi</param>
        /// <returns></returns>
        public static bool IsPhoneNumber(this string input)
        {
            var regex = new Regex(@"(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)([0-9]{9}|[0-9\-\s]{9,18})", RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Đọc dung lượng file thành chuỗi
        /// </summary>
        /// <param name="filesize">Dung lượng</param>
        /// <returns></returns>
        public static string ReadFileSize(decimal filesize)
        {
            const decimal oneKiloByte = 1024;
            const decimal oneMegaByte = 1048576;
            const decimal oneGigaByte = 1073741824;
            if (filesize >= oneGigaByte)
            {
                return (filesize / oneGigaByte).ToString("0.00", CultureInfo.InvariantCulture) + " GB";
            }
            if (filesize >= oneMegaByte)
            {
                return (filesize / oneMegaByte).ToString("0.00", CultureInfo.InvariantCulture) + " MB";
            }
            if (filesize >= oneKiloByte)
            {
                return (filesize / oneKiloByte).ToString("0", CultureInfo.InvariantCulture) + " KB";
            }
            return filesize + " bytes";
        }

        public static string ToUrlString(this string input)
        {
            string str = (new Regex(@"[-]{2}")).Replace((new Regex(@"[^\w]")).Replace(input.ToLower().StripVietnameseChars(), "-"), "-");
            string[] arrListStr = str.Split('-');
            for (int i = 0; i < arrListStr.Length; i++)
            {
                str = str.Replace("--", "-");
            }
            if (str.StartsWith("-")) str = str.Substring(1);
            if (str.EndsWith("-")) str = str.TrimEnd(str[str.Length - 1]);
            return str;
        }

        public static string ToPlainText(this string str)
        {
            return string.IsNullOrEmpty(str) ? string.Empty : Regex.Replace(str, @"<(.|\n)*?>", "");
        }

        /// <summary>
        /// Convert first char to lower case. It usefull for convert entity object properties to mongodb properties
        /// </summary>
        /// <param name="str">String that want to convert</param>
        /// <returns></returns>
        public static string ConvertFirstCharToLowerCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
                return str;

            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static string ColumnAdress(int col)
        {
            if (col <= 26)
            {
                return Convert.ToChar(col + 64).ToString();
            }
            int div = col / 26;
            int mod = col % 26;
            if (mod == 0) { mod = 26; div--; }
            return ColumnAdress(div) + ColumnAdress(mod);
        }

        public static int ColumnNumber(string colAdress)
        {
            int[] digits = new int[colAdress.Length];
            for (int i = 0; i < colAdress.Length; ++i)
            {
                digits[i] = Convert.ToInt32(colAdress[i]) - 64;
            }
            int mul = 1; int res = 0;
            for (int pos = digits.Length - 1; pos >= 0; --pos)
            {
                res += digits[pos] * mul;
                mul *= 26;
            }
            return res;
        }

        public static string ToUnsignString(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            input = input.Replace("  ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-").ToLower();
            }
            return str2;
        }
        public static string ToString(decimal number)
        {
            string s = number.ToString("#");
            string[] numberWords = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] layer = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, unit, dozen, hundred;
            string str = " ";
            bool booAm = false;
            decimal decS = 0;
            //Tung addnew
            try
            {
                decS = Convert.ToDecimal(s.ToString());
            }
            catch
            {
            }
            if (decS < 0)
            {
                decS = -decS;
                s = decS.ToString();
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                str = numberWords[0] + str;
            else
            {
                j = 0;
                while (i > 0)
                {
                    unit = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        dozen = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        dozen = -1;
                    i--;
                    if (i > 0)
                        hundred = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        hundred = -1;
                    i--;
                    if ((unit > 0) || (dozen > 0) || (hundred > 0) || (j == 3))
                        str = layer[j] + str;
                    j++;
                    if (j > 3) j = 1;
                    if ((unit == 1) && (dozen > 1))
                        str = "một " + str;
                    else
                    {
                        if ((unit == 5) && (dozen > 0))
                            str = "lăm " + str;
                        else if (unit > 0)
                            str = numberWords[unit] + " " + str;
                    }
                    if (dozen < 0)
                        break;
                    else
                    {
                        if ((dozen == 0) && (unit > 0)) str = "lẻ " + str;
                        if (dozen == 1) str = "mười " + str;
                        if (dozen > 1) str = numberWords[dozen] + " mươi " + str;
                    }
                    if (hundred < 0) break;
                    else
                    {
                        if ((hundred > 0) || (dozen > 0) || (unit > 0)) str = numberWords[hundred] + " trăm " + str;
                    }
                    str = " " + str;
                }
            }
            if (booAm) str = "Âm " + str;
            return Regex.Replace(str + "đồng chẵn", @"\s+", " ").Trim();
        }
    }
}
