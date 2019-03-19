using System.Drawing;

namespace MrBildo.DMSounds
{
	public interface ITheme
	{
		Color BackgroundColor { get; set; }

		Color ForegroundColor { get; set; }

		byte[] BackgroundImage { get; set; }

		byte[] Icon { get; set; }
	}
}