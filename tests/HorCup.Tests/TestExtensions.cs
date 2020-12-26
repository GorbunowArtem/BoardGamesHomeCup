using System;
using System.Collections.Generic;
using System.Linq;

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

		public static Guid? Guid(this int? id)
		{
			if (id == null)
			{
				return null;
			}
			
			var bytes = new byte[16];
			BitConverter.GetBytes(id.Value).CopyTo(bytes, 0);
			return new Guid(bytes);
		}

		public static Guid[] ToGuidArray(this IEnumerable<int> ids)
		{
			return ids.Select(id => id.Guid()).ToArray();
		}
		
		public static DateTime ToDateTime(
			int year,
			int month,
			int day) => new DateTime(year, month, day, 0, 0, 0);
	}
}