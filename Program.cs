using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace arbetsprob
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var grid = GetTestGrid();

            // var output = new StringBuilder('|');
            // foreach (var col in grid.Columns)
            // {
            //     output.Append(col.Header);
            //     output.Append(' ', col.Width - col.Header.Length);
            //     output.Append('|');
            // }
            // output.Append(Environment.NewLine);
            // output.AppendLine("En till rad");
            // output.Append("Sist.");
            // Console.Write(output.ToString());
            // Console.ResetColor();
            // for (var i = 0; i < 10; i++)
            // {
            RenderGrid(grid);

            // }
            //Console.Read();
        }

        public static void RenderGrid(Grid grid)
        {
            const char topBorder = '_';
            const char leftBorder = '|';
            const char rightBorder = '|';
            const char bottomBorder = '-';
            var contentWidth = grid.Columns.Sum(x => x.Width) + ((grid.Columns.Count() - 1) * 3) + 2;

            // var header = new StringBuilder();
            // header.Append('˧');
            // header.Append(topBorder, contentWidth);
            // header.Append('˧');
            // Console.WriteLine(header);

            // header.Clear();
            // header.Append(leftBorder);
            // for (var i = 0; i < grid.Columns.Count; i++)
            // {
            //     var col = grid.Columns[i];
            //     header.Append(col.Header);
            //     header.Append(' ', col.Width - col.Header.Length);
            //     if (i < grid.Columns.Count - 1) header.Append('|');
            // }
            // header.Append(rightBorder);

            //Console.WriteLine('┏' + new string('━', contentWidth) + '┓');
            Console.WriteLine('┏' + string.Join("┳", grid.Columns.Select((col, i) => new string('━', col.Width + 2))) + '┓');
            var headerContent = string.Join(" ┃ ", grid.Columns.Select((col, i) => RenderColData(col, col.Header, true)));
            Console.WriteLine("┃ " + headerContent + " ┃");
            Console.WriteLine('┣' + string.Join("╋", grid.Columns.Select((col, i) => new string('━', col.Width + 2))) + '┫');

            //Console.WriteLine('┣' + new string('━', contentWidth) + '┫');
            // Console.BackgroundColor = ConsoleColor.DarkBlue;
            // Console.ForegroundColor = ConsoleColor.Gray;
            Console.ResetColor();
            for (var r = 0; r < grid.Rows.Count; r++)
            {
                var rowContent = string.Join(" ┃ ", grid.Columns.Select((col, i) => RenderColData(col, grid.Rows[r][i])));
                Console.WriteLine("┃ " + rowContent + " ┃");
            }
            Console.WriteLine('┗' + string.Join('┻', grid.Columns.Select((col, i) => new string('━', col.Width + 2))) + '┛');

            // Render header
        }

        private static string Spaces(int len) { return new string(' ', len); }
        private static string RenderColData(Column column, string value, bool header = false)
        {
            var spaceCount = column.Width - value.Length;
            if (header)
            {
                return value.ToUpper() + Spaces(spaceCount);
            }
            if (column.Alignment == Alignment.Left)
            {
                return value + Spaces(spaceCount);
            }
            return Spaces(spaceCount) + value;
        }

        private static string AlignText(string text, Alignment alignment, int width)
        {
            var spaceCount = width - text.Length;
            if (alignment == Alignment.Left)
            {
                return text + Spaces(spaceCount);
            }
            return Spaces(spaceCount) + text;
        }
        // private static string RenderColData(Column column, string value)
        // {
        //     var spaces = column.Width - value.Length;
        //     var sb = new StringBuilder();
        //     if (column.Alignment == Alignment.Left)
        //     {
        //         sb.Append(value);
        //         sb.Append(' ', spaces);
        //     }
        //     else
        //     {
        //         sb.Append(' ', spaces);
        //         sb.Append(value);
        //     }
        //     return sb.ToString();
        // }

        private void RenderGridHeader(List<Column> columns)
        {

        }

        private static Grid GetTestGrid()
        {
            var grid = new Grid()
            {
                Columns = new List<Column>() {
                    new Column(1, "Id", 3, Alignment.Right),
                    new Column(2, "Name", 20),
                    new Column(3, "Regline", 13),
                    new Column(4, "Time", 5),
                    new Column(5, "OrderId", 7, Alignment.Right),
                    new Column(6, "Invoice", 7, Alignment.Right)
                },
                Rows = new List<string[]>() {
                    Row(1, "Peter Stormare", "Visitor", "14:45", 51325, "▩ ▢"),
                    Row(2, "Mary Louise Parker", "Exhibitor", "14:54", 51341, "✓"),
                    Row(3, "Claire Danes", "Exhibitor", "08:11", 50933,"░░"),
                    Row(4, "Mark Wahlberg", "VIP", "20:30", 51333, "✓"),
                    Row(5, "Seth Romatelli", "Visitor", "13:04", 54012, ""),
                }
            };
            return grid;
        }

        private static string[] Row(params object[] colData)
        {
            return colData.Select(x => x.ToString()).ToArray();
        }
    }

    // public static class StaticHelpers
    // {
    //     public static void AddColData(this StringBuilder sb, Column column, string value)
    //     {
    //         var spaces = column.Width - value.Length;
    //         if (column.Alignment == Alignment.Left)
    //         {
    //             sb.Append(value);
    //             sb.Append(' ', spaces);
    //         }
    //         else
    //         {
    //             sb.Append(' ', spaces);
    //             sb.Append(value);
    //         }
    //     }
    // }

    public class Grid
    {
        public List<Column> Columns { get; set; }
        public List<string[]> Rows { get; set; }
    }

    public class Column
    {
        public Column(int id, string header, int width = 10, Alignment align = Alignment.Left)
        {
            Id = id;
            Header = header;
            Width = width;
            Alignment = align;
        }
        public int Id { get; set; }
        public string Header { get; set; }
        public int Width { get; set; }
        public Alignment Alignment { get; set; }
    }

    public enum Alignment
    {
        Left = 1,
        Center = 2,
        Right = 3
    }
}
