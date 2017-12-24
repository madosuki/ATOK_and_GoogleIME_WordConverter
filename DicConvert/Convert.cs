using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DicConvert
{
    class Convert
    {
        // Google IME辞書からATOK辞書変換テーブル
        private Dictionary<string, string> google_to_atok;

        // ATOK辞書からGoogle IME辞書変換テーブル
        private Dictionary<string, string> atok_to_google;

        // Google IME品詞特有の物で変換不能な物
        private List<string> removeword_google;

        // ATOK品詞特有の物で変換不能な物
        private List<string> removeword_atok;

        private string[] arg;

        public Convert(string[] args)
        {
            Init(args);
        }

        public void Start()
        {

            string msg = "変換元のIMEを表示されている数字で入力してください\n(1) ATOK\n(2) Google IME";
            Console.WriteLine(msg);
            var select = Console.ReadLine();

            foreach (var p in arg)
            {
                if (select == "1")
                {
                    var rs = new System.IO.StreamReader(p, Encoding.GetEncoding("shift_jis"));
                    Read(p, rs, Int32.Parse(select));
                }
                else if (select == "2")
                {
                    var rs = new System.IO.StreamReader(p, Encoding.GetEncoding("utf-8"));
                    Read(p, rs, Int32.Parse(select));
                }
                else
                {
                    Console.WriteLine("1、又は2を入力してください");
                }
            }

        }

        private void Read(string path, System.IO.StreamReader rs, int mode)
        {
            string text = "";
            if (mode == 2)
            {
                while (rs.Peek() > -1)
                {
                    text += Googletoatok(rs.ReadLine()) + "\r\n";
                }
            }
            else
            {
                while (rs.Peek() > -1)
                {
                    text += Atoktogoogle(rs.ReadLine()) + "\n";
                }
            }

            Write(path, text, mode);
        }

        private void Write(string path, string text, int select)
        {
            string finalpath = "converted_" + System.IO.Path.GetFileName(path);
            if (select == 2)
            {
                System.IO.File.WriteAllText(finalpath, text, Encoding.GetEncoding("shift_jis"));
            }
            else
            {
                System.IO.File.WriteAllText(finalpath, text, Encoding.GetEncoding("utf-8"));
            }
            // Console.WriteLine(text);
            // Console.WriteLine(finalpath);
        }

        private string Googletoatok(string str)
        {
            string tmp = "";
            const string pattern = @"(\w+\s+\w+\s+)(";

            foreach (var i in google_to_atok)
            {
                string tmppattern = pattern + i.Key + @"\s" + ")";
                var rgx = new Regex(tmppattern);
                if (rgx.IsMatch(str))
                {
                    string replacestr = @"$1" + i.Value;
                    tmp = rgx.Replace(str, replacestr);
                    return tmp;
                }
            }

            foreach (var n in removeword_google)
            {
                string removepattern = pattern + n + ")";
                var removergx = new Regex(removepattern);
                if (!removergx.IsMatch(str))
                {
                    return str;
                }
            }

            return tmp;
        }

        private string Atoktogoogle(string str)
        {
            string processed = str.Trim('*');

            string tmp = "";
            const string pattern = @"(\w+\s+\w+\s+)";
            foreach (var i in atok_to_google)
            {
                string tmppattern = pattern + i.Key;
                var rgx = new Regex(tmppattern);
                if (rgx.IsMatch(processed))
                {
                    string replacestr = @"$1" + i.Value;
                    tmp = rgx.Replace(processed, replacestr);
                    return tmp;
                }
            }

            foreach (var n in removeword_atok)
            {
                string removepattern = pattern + n;
                var removergx = new Regex(removepattern);
                if (!removergx.IsMatch(processed))
                {
                    return processed;
                }
            }

            return tmp;
        }

        private void Init(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("引数がありません。ファイルのパスをコマンドラインから引数として渡すか変換元ファイルをD&Dしてください");
                Environment.Exit(0);
            }
            else
            {
                arg = args;
            }

            google_to_atok = new Dictionary<string, string>()
            {
                { "短縮読み", "名詞" },
                { "サジェストのみ", "名詞" },
                { "固有名詞", "名詞" },
                { "人名", "固有人他" },
                { "姓", "固有人姓" },
                { "名", "固有人名" },
                { "組織", "固有組織" },
                { "地名", "固有地名" },
                { "数", "名詞" },
                { "アルファベット", "名詞" },
                { "記号", "名詞" },
                { "接尾一般", "接頭語" },
                { "接尾人名", "接頭語" },
                { "接尾地名", "接頭語" },
                { "動詞ワ行五段", "ワ行五段" },
                { "動詞カ行五段", "カ行五段" },
                { "動詞タ行五段", "タ行五段" },
                { "動詞ナ行五段", "ナ行五段" },
                { "動詞マ行五段", "マ行五段" },
                { "動詞ラ行五段", "ラ行五段" },
                { "動詞ガ行五段", "ガ行五段" },
                { "動詞バ行五段", "バ行五段" },
                { "動詞一段", "一段動詞" },
                { "動詞カ変", "カ変動詞" },
                { "動詞サ変", "サ変動詞" },
                { "動詞ザ変", "ザ変動詞" }
            };

            removeword_google = new List<string>()
            {
                "句読点",
                "抑制単語"
            };

            removeword_atok = new List<string>()
            {
                "ワ行五段音便",
                "ナ変動詞",
                "カ行上二段",
                "ガ行上二段",
                "タ行上二段",
                "ダ行上二段",
                "ハ行上二段",
                "バ行上二段",
                "マ行上二段",
                "ヤ行上二段",
                "ラ行上二段",
                "カ行下二段",
                "ガ行下二段",
                "サ行下二段",
                "ザ行下二段",
                "タ行下二段",
                "ダ行下二段",
                "ナ行下二段",
                "ハ行下二段",
                "バ行下二段",
                "マ行下二段",
                "ヤ行下二段",
                "ラ行下二段",
                "ワ行下二段",
            };

            atok_to_google = new Dictionary<string, string>()
            {
                { "固有人姓", "姓" },
                { "固有人名", "名" },
                { "固有人他", "人名" },
                { "固有地名", "地名" },
                { "固有組織", "組織" },
                { "固有商品", "固有名詞" },
                { "固有一般", "固有名詞" },
                { "名詞ザ変", "名詞" },
                { "形容詞ウ", "形容詞" },
                { "形容詞イ", "形容詞" },
                { "形容詞エ", "形容詞" },
                { "形容動詞", "形容詞" },
                { "形動タリ", "形容詞" },
                { "カ行五段", "動詞カ行五段" },
                { "ガ行五段", "動詞ガ行五段" },
                { "サ行五段", "動詞サ行五段" },
                { "タ行五段", "動詞タ行五段" },
                { "ナ行五段", "動詞ナ行五段" },
                { "バ行五段", "動詞バ行五段" },
                { "マ行五段", "動詞マ行五段" },
                { "ラ行五段", "動詞ラ行五段" },
                { "ワ行五段", "動詞ワ行五段" },
                { "ハ行四段", "動詞ハ行四段" },
                { "一段動詞", "動詞一段" },
                { "カ変動詞", "動詞カ変" },
                { "サ変動詞", "動詞サ変" },
                { "ザ変動詞", "動詞ザ変" },
                { "ラ変動詞", "動詞ラ変" },
                { "数詞", "名詞" },
                { "冠数詞", "名詞" },
                { "接尾語", "接尾一般" },
                { "助数詞", "名詞" },
                { "単漢字", "名詞" }
            };

        }
    }
}
