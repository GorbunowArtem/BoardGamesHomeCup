using System;

namespace HorCup.Tests
{
	public static class TestExtensions
	{
		public static Guid Guid(this int id)
		{
			var bytes = new byte[16];
			BitConverter.GetBytes(id).CopyTo(bytes, 0);
			return new Guid(bytes);
		}
	}
}