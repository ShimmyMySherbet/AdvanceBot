using AdvanceEngine.Models.Enums;

namespace AdvanceEngine
{
	public static class Extensions
	{
		public static ETeam Enemy(this ETeam team)
		{
			if (team == ETeam.Black)
			{
				return ETeam.White;
			}
			else if (team == ETeam.White)
			{
				return ETeam.Black;
			}
			return ETeam.Neutral;
		}
	}
}
