namespace Xylia.Match.Windows
{
	public class ControlPage
	{
		public ControlPage(string text, Control control)
		{
			this.Key = control.GetType().Name;

			this.Text = text;
			this.Control = control;
		}

		public string Key;
		public string Text;
		public Control Control;
	}
}