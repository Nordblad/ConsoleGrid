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
            RenderGrid(grid);
        }

        public static void RenderGrid(Grid grid)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var contentWidth = grid.Columns.Sum(x => x.Width) + ((grid.Columns.Count() - 1) * 3) + 2;

            Console.WriteLine('┏' + string.Join("┳", grid.Columns.Select(col => new string('━', col.Width + 2))) + '┓');
            Console.WriteLine("┃ " + string.Join(" ┃ ", grid.Columns.Select(col => AlignText(col.Header, Alignment.Left, col.Width))) + " ┃");
            Console.WriteLine('┣' + string.Join("╋", grid.Columns.Select(col => new string('━', col.Width + 2))) + '┫');
            for (var r = 0; r < grid.Rows.Count; r++)
            {
                var rowContent = string.Join(" ┃ ", grid.Columns.Select((col, i) => AlignText(grid.Rows[r][i], col.Alignment, col.Width)));
                Console.WriteLine("┃ " + rowContent + " ┃");
            }
            Console.WriteLine('┗' + string.Join("┻", grid.Columns.Select(col => Repeat('━', col.Width + 2))) + '┛');
        }
        private static string Spaces(int count) { return Repeat(' ', count); }
        private static string Repeat(char c, int count) { return new string(c, count); }
        private static string AlignText(string text, Alignment alignment, int width)
        {
            var spaceCount = width - text.Length;
            if (alignment == Alignment.Left)
            {
                return text + Spaces(spaceCount);
            }
            return Spaces(spaceCount) + text;
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
                    new Column(6, "Invoice", 7, Alignment.Right),
                    new Column(7, "Total", 12, Alignment.Right)
                },
                Rows = new List<string[]>() {
                    Row(1, "Peter Stormare", "Visitor", "14:45", 51325, "", "2 149.00:-"),
                    Row(2, "Mary Louise Parker", "Exhibitor", "14:54", 51341, "✓", "0.00:-"),
                    Row(3, "Claire Danes", "Exhibitor", "08:11", 50933,"", "459.95:-"),
                    Row(4, "Mark Wahlberg", "VIP", "20:30", 51333, "✓", "459.95:-"),
                    Row(5, "Seth Romatelli", "Visitor", "13:04", 54012, "", "59.95:-"),
                }
            };
            return grid;
        }

        private static string[] Row(params object[] colData)
        {
            return colData.Select(x => x.ToString()).ToArray();
        }
    }

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
        //Center = 2,
        Right = 3
    }
}
