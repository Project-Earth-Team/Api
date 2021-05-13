using System;
using ProjectEarthServerAPI.Models;
using Serilog;

namespace ProjectEarthServerAPI.Util
{
	public class EventUtils
	{
		public static void HandleEvents(string playerId, BaseEvent genoaEvent)
		{
			switch (genoaEvent)
			{
				case ItemEvent ev:
					Log.Debug("[System] Item Event dispatched!");
					Log.Debug(ev.action.ToString());
					ChallengeUtils.ProgressChallenge(playerId, genoaEvent);

					break;

				case MultiplayerEvent ev:
					Log.Debug("[System] Multiplayer Event dispatched!");
					break;

				case ChallengeEvent ev:
					Log.Debug("[System] Challenge Event dispatched!");
					ChallengeUtils.ProgressChallenge(playerId, genoaEvent);
					break;

				case MobEvent ev:
					Log.Debug("[System] Mob Event dispatched!");
					break;

				case TappableEvent ev:
					Log.Debug("[System] Tappable Event dispatched!");

					var tappable = StateSingleton.Instance.activeTappables[ev.eventId];
					Log.Debug($"[System] Tappable Type: {tappable.location.type}");
					Log.Debug($"[System] Tappable Location: {tappable.location.tileId}");

					ChallengeUtils.ProgressChallenge(playerId, genoaEvent);

					break;

				default:
					Log.Error("Error: Something tried to fire a normal BaseEvent!");
					break;
			}
		}
	}
}
