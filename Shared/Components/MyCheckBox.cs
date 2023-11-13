namespace PasswordGeneratorWasm.Shared
{
	public class MyCheckBox
	{
		protected string _id = "";
		protected bool _enabled = true;
		public string Id
		{
			get
			{
				if (string.IsNullOrEmpty(_id)) _id = autoId();
				return _id;
			}
			set
			{
				_id = value.Trim();
				if (string.IsNullOrEmpty(_id)) _id = autoId();
			}
		}

		public string Value { get; set; } = "";
		public string Text { get; set; } = "";
		public bool Checked { get; set; } = true;
		public bool Enabled { get; set; } = true;
		public MyCheckBox(string id = "", string text = "", string value = "", bool @checked = true, bool enabled = true)
		{
			if (string.IsNullOrEmpty(id))
			{
				_id = autoId();
				Text = _id;
			}
			else
			{
				Id = id;
				Text = text;
			}

			Value = value;
			Checked = @checked;
			Enabled = enabled;
		}

		protected string autoId()
		{
			int rnd = new Random().Next(10000);
			return $"MyChkBox_{DateTime.Now:HHmmss}{rnd}";
		}
		public string Bool2Text(bool b) => b ? "true" : "false";

	}
}
