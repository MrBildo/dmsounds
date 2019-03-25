using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.IO
{
	public static class FileAsync
	{
		public static async Task<string> ReadAllTextAsync(string path)
		{
			using(var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
			{
				var sb = new StringBuilder();

				var buffer = new byte[0x1000];

				var read = 0;

				while ((read = await fs.ReadAsync(buffer, 0, buffer.Length)) != 0)
				{
					var text = Encoding.Unicode.GetString(buffer, 0, read);

					sb.Append(text);
				}

				return sb.ToString();
			}
		}

		public static async Task WriteAllTextAsync(string path, string contents)
		{
			var data = Encoding.UTF8.GetBytes(contents);

			using(var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 4096, true))
			{
				await fs.WriteAsync(data, 0, data.Length);
			}
		}
	}
}
