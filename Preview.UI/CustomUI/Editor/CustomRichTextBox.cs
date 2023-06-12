/****************************** Module Header ******************************\
* Module Name:      CustomRichTextBox.cs
* Project:                   CSWinFormSearchAndHighlightText
* Copyright(c)          Microsoft Corporation.
* 
* Theclass is used to create custom RichTextBox
* The custom RichTextBox add custom Highlight and ClearHighlight method
*
* This source is subject to the Microsoft Public License.
* See http://www.microsoft.com/en-us/openness/licenses.aspx.
* All other rights reserved.
* 
* THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
* EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
* WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using Xylia.Windows;
using Xylia.Windows.Forms.Editor;

namespace Crypto_Notepad
{
    /// <summary>
    ///  Custom RichTextBox control
    /// </summary>
    public partial class CustomRichTextBox : RichTextBox
    {
        public CustomRichTextBox()
            : base()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        //this message is sent to the control when we scroll using the mouse
        private const int WM_MOUSEWHEEL = 0x20A;

        //and this one issues the control to perform scrolling
        private const int WM_VSCROLL = 0x115;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!base.AutoWordSelection)
            {
                base.AutoWordSelection = true;
                base.AutoWordSelection = false;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MOUSEWHEEL)
            {
                int scrollLines = SystemInformation.MouseWheelScrollLines;
                for (int i = 0; i < scrollLines; i++)
                {
                    try
                    {
                        if ((int)m.WParam >= 0) // when wParam is greater than 0
                            SendMessage(this.Handle, WM_VSCROLL, (IntPtr)0, IntPtr.Zero); // scroll up 
                        else
                            SendMessage(this.Handle, WM_VSCROLL, (IntPtr)1, IntPtr.Zero); // scroll down
                    }
                    catch (OverflowException)
                    {
                        SendMessage(this.Handle, WM_VSCROLL, (IntPtr)1, IntPtr.Zero); // scroll down
                    }
                }
                return;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        ///  高亮显示搜索内容
        /// </summary>
        /// <param name="findWhat">内容</param>
        /// <param name="highlightColor">Highlight Color</param>
        /// <param name="ismatchCase">Is Match Case</param>
        /// <param name="ismatchWholeWord">Is Match Whole Word</param>
        /// <returns></returns>
        public List<int> Highlight(string findWhat, Color highlightColor, bool ismatchCase, bool ismatchWholeWord)
        {
            List<int> list = new List<int>();

            ClearHighlight();
            int startSearch = 0;
            //int searchLength = findWhat.Length;
            RichTextBoxFinds findoptions = default(RichTextBoxFinds);

            // Setup the search options.
            if (ismatchCase && ismatchWholeWord)
            {
                findoptions = RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord;
            }
            else if (ismatchCase)
            {
                findoptions = RichTextBoxFinds.MatchCase;
            }
            else if (ismatchWholeWord)
            {
                findoptions = RichTextBoxFinds.WholeWord;
            }
            else
            {
                findoptions = RichTextBoxFinds.None;
            }

            // detect whether search text exists in richtextbox
          
            int index = -1;

            // Search text in RichTextBox and highlight them with color.

            int max = this.TextLength;

            while (startSearch < max && (index = this.Find(findWhat, startSearch, findoptions)) > -1)
            {
                list.Add(startSearch);

                this.SelectionBackColor = highlightColor;
                //继续寻找下一部分
                startSearch = index + 1;
            }

            return list;
        }

        /// <summary>
        /// 清理所有高亮内容
        /// </summary>
        private void ClearHighlight()
        {
            Settings ps =Settings.Default;
            //this.SelectAll();
            this.SelectionBackColor = ps.RichBackColor;
        }
    }
}
