
/// <summary>
/// 正長方形のクラス
/// </summary>
class Rect
{
    public int Width { get; set; } // 長方形の横幅
    public int Height { get; set; } // 長方形の高さ
    public int Distance { get; set; } // 長方形の対角線の長さ

    public Rect(int height, int width)
    {
        Width = width;
        Height = height;
        Distance = width * width + height * height;
    }

    public override string ToString()
    {
        return $"縦:{Height} 横:{Width} 対角:{Distance}";
    }
}

class Program
{
    /// <summary>
    /// 正長方形を大きさの定義に従って比較するメソッド
    /// </summary>
    /// <param name="r1">比較する正長方形R1</param>
    /// <param name="r2">比較する正長方形R2</param>
    /// <returns>R1のほうが大きいときは正の値、R2のほうが大きいときは負の値、大きさが等しいときは0を返す</returns>
    static int CompareRect(Rect r1, Rect r2)
    {
        int d = r1.Distance - r2.Distance;
        if (d == 0)
        {
            if (r1.Height > r2.Height)
                return 1;
            else if (r1.Height < r2.Height)
                return -1;
            else
                return 0;
        }
        else
        {
            return d;
        }
    }

    /// <summary>
    /// 一つ大きいサイズの正長方形を探し出力するメソッド
    /// </summary>
    /// <param name="lst">一つ大きいサイズを探したい正長方形のリスト</param>
    static void FindNextRectangle(List<Rect> lst)
    {
        List<Rect> data = new List<Rect>();
        for (int w1 = 2; w1 <= 150; w1++)
        {
            for (int h1 = 1; h1 < w1; h1++)
            {
                data.Add(new Rect(h1, w1));
            }
        }

        var sortStopWatch = new System.Diagnostics.Stopwatch();
        Console.WriteLine("ソート開始");
        sortStopWatch.Start();
        data.Sort(CompareRect);
        sortStopWatch.Stop();
        Console.WriteLine($"ソート終了  {sortStopWatch.ElapsedMilliseconds}ms");

        var searchStopWatch = new System.Diagnostics.Stopwatch();
        searchStopWatch.Start();
        string output = "";

        foreach (Rect x in lst)
        {
            int lower = 0;
            int upper = data.Count - 1;
            while (true)
            {
                int center = (upper - lower) / 2 + lower;
                if (CompareRect(x, data[center]) == 0)
                {
                    if (center == data.Count - 1)
                        break;
                    output += $"{data[center + 1].Height} {data[center + 1].Width}\n";
                    break;
                }
                else if (CompareRect(x, data[center]) < 0)
                {
                    upper = center - 1;
                }
                else
                {
                    lower = center + 1;
                }
            }
        }

        searchStopWatch.Stop();
        Console.WriteLine(output + $"検索終了 {searchStopWatch.ElapsedMilliseconds}ms");
        File.WriteAllText("output.txt", output);
    }

    /// <summary>
    /// テキストデータから正長方形クラスのリストとして読み込むメソッド。(読み込み先は"input.txt")
    /// </summary>
    /// <returns>読み込んだ正長方形のリスト</returns>
    static List<Rect> ReadTxt()
    {
        List<Rect> data = new List<Rect>();
        using (StreamReader sr = new StreamReader("input.txt"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] s = line.Split(" ");
                int h = int.Parse(s[0]);
                int w = int.Parse(s[1]);
                if (h == 0 && w == 0)
                    break;
                data.Add(new Rect(h, w));
            }

        }
        return data;
    }

    static void Main(string[] args)
    {
        List<Rect> lst = ReadTxt();
        FindNextRectangle(lst);
    }
}