using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Com.IT.DiPaSport.View.Controls
{
    public class TextBlockEx
    {
        public TextBlock MakeFormatTextBlock(string html)
        {
            TextBlock tb = new TextBlock();
            Run tempRun = new Run();
            int strike = 0;

            do
            {
                if (html.StartsWith("<strike>") | html.EndsWith("</strike>"))
                {
                    strike += html.StartsWith("<strike>") ? 1 : 0;
                    strike -= html.EndsWith("</strike>") ? 1 : 0;
                    if (tempRun.Text != null)
                    {
                        tb.Inlines.Add(tempRun);
                    }

                    tempRun = new Run();
                }
            } while (html.Length > 0);

            return tb;
        }
    }
}
