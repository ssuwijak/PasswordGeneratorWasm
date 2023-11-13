using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace PasswordGeneratorWasm.Shared
{
	public class PwdGenerator
	{
		internal class AsciiCode
		{
			public const int MIN = 0;
			public const int MAX = 255;
			public static bool IsInvalid(int asciiCode) => (asciiCode < MIN) || (asciiCode > MAX);
			public static bool IsValid(int asciiCode) => !IsInvalid(asciiCode);
		}
		internal class Length
		{
			public const int Min = 1;
			public const int Max = 32;
			public static int Validate(int length)
			{
				if (length < Length.Min) length = Length.Min;
				if (length > Length.Max) length = Length.Max;

				return length;
			}
		}
		public string Digits { get; private set; }
		public string Lowercase { get; private set; }
		public string Uppercase { get; private set; }
		public string Symbols { get; private set; }
		public string Punctuation { get; private set; }
		public string WhiteSpace { get; private set; }
		public string Letters { get; private set; }
		public string Printable { get; private set; }
		public PwdGenerator()
		{
			Digits = getChars((int)'0', (int)'9');
			Lowercase = getChars((int)'a', (int)'z');
			Uppercase = getChars((int)'A', (int)'Z');
			Symbols = "!@#$%^&*";

			Punctuation = set_Punctuation();
			WhiteSpace = set_WhiteSpace();
			Letters = Lowercase + Uppercase;
			Printable = Digits + Letters + Punctuation;
		}
		protected string getChars(int code1, int code2, string separator = "")
		{
			if (AsciiCode.IsInvalid(code1)) return string.Empty;
			if (AsciiCode.IsInvalid(code2)) return string.Empty;

			if (code1 > code2)
			{
				int tmp = code1;
				code1 = code2;
				code2 = tmp;
			}

			List<char> charList = new List<char>();

			for (int i = code1; i <= code2; i++)
				charList.Add((char)i);

			return string.Join(separator, charList);
		}
		protected string set_Punctuation()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(getChars(33, 47));
			sb.Append(getChars(58, 64));
			sb.Append(getChars(91, 96));
			sb.Append(getChars(123, 126));

			return sb.ToString();
		}
		protected string set_WhiteSpace()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(getChars(9, 12));
			sb.Append(" ");// add a space

			return sb.ToString();
		}
		protected string validateModes(string modes, int pwdLength = 0)
		{
			modes = modes.Trim();

			if (string.IsNullOrEmpty(modes)) modes = "1";
			if (modes.Length > 4) modes = modes.Substring(0, 4);

			int[] modeTypes = { 0, 0, 0, 0 };

			foreach (char m in modes)
			{
				if (Lowercase.Contains(m))
					modeTypes[1] += 1;
				else if (Uppercase.Contains(m))
					modeTypes[2] += 1;
				else if (Symbols.Contains(m))
					modeTypes[3] += 1;
				else
					modeTypes[0] += 1;
			}

			string modesNew = "";

			if (modeTypes[0] > 0) modesNew += "1";
			if (modeTypes[1] > 0) modesNew += "a";
			if (modeTypes[2] > 0) modesNew += "A";
			if (modeTypes[3] > 0) modesNew += "#";

			return modesNew;
		}
		public string Generate(int pwdLength, string modes = "")
		{
			string pwd = generate(pwdLength, modes);
			return SuffleString(pwd);
		}
		protected string generate(int pwdLength, string modes = "")
		{
			pwdLength = Length.Validate(pwdLength);
			modes = validateModes(modes, pwdLength);

			int modesLength = modes.Length;

			if (pwdLength < modesLength) pwdLength = modesLength;

			StringBuilder sb = new StringBuilder();
			Random rnd = new Random();

			string mode = "";
			int i, j;

			for (i = 0; i < pwdLength; i++)
			{
				if (i < modesLength)
					mode = modes.Substring(i, 1);
				else
				{
					j = rnd.Next(modesLength);
					mode = modes.Substring(j, 1);
				}

				if (Lowercase.Contains(mode))
				{
					j = rnd.Next(Lowercase.Length);
					sb.Append(Lowercase.Substring(j, 1));
				}
				else if (Uppercase.Contains(mode))
				{
					j = rnd.Next(Uppercase.Length);
					sb.Append(Uppercase.Substring(j, 1));
				}
				else if (Symbols.Contains(mode))
				{
					j = rnd.Next(Symbols.Length);
					sb.Append(Symbols.Substring(j, 1));
				}
				else
				{
					j = rnd.Next(Digits.Length);
					sb.Append(Digits.Substring(j, 1));
				}
			}

			return sb.ToString();
		}
		public string SuffleString(string str)
		{
			str = str.Trim();

			if (String.IsNullOrEmpty(str)) return str;
			if (str.Length < 2) return str;

			int i, j, k;

			char tmp = '\0';
			char[] suffledChars = str.ToCharArray();

			j = suffledChars.Length;

			Random rnd = new Random();

			for (i = 0; i < j; i++)
			{
				k = rnd.Next(i, j);
				tmp = suffledChars[i];
				suffledChars[i] = suffledChars[k];
				suffledChars[k] = tmp;
			}

			return string.Join("", suffledChars);
		}


	}
}
