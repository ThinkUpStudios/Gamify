package ar.com.mdf.gamify.objects.notification;

import com.google.gson.annotations.SerializedName;

public class ConnectionStatePlayerNotificationObject extends NotificationObject {
	
	@SerializedName("PlayerName")
	private String playerName;

	public String getPlayerName() {
		return playerName;
	}

	public void setPlayerName(String playerName) {
		this.playerName = playerName;
	}	
}
