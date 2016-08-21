using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class LeaderBoardAPI
{
	private const string SCORE_URI = "http://128.199.56.123:4570/score";
	private const string TOP_URI = "http://128.199.56.123:4570/top";

	[Serializable]
	public class LeaderBoardResponse
	{
		[Serializable]
		public class Score
		{
			public string id;
			public String email;
			public int position;
			public int score;
		}

		public String status;
		public Score[] top = new Score[10];
	}

	[Serializable]
	class ScoreRequest
	{
		int score;

		public ScoreRequest (int score)
		{
			this.score = score;
		}

		public String toJson ()
		{
			return "{ \"score\":" + this.score + "}";
		}
	}



	public static IEnumerator getLeaders (Action<LeaderBoardResponse> onSuccess, Action<String> onError)
	{
		Dictionary<string, string> headers = new Dictionary<string, string> ();
		headers.Add ("Content-Type", "application/json");
		headers.Add ("Token", AppGlobal.token);

		WWW request = new WWW (TOP_URI, null, headers);
		yield return request;

		if (request.error != null) {
			Debug.Log ("request error: " + request.error);
			onError.Invoke (request.error);
		} else {
			Debug.Log ("request data: " + request.text);
			LeaderBoardResponse response = JsonUtility.FromJson<LeaderBoardResponse> (request.text);
			onSuccess.Invoke (response);
		}
	}

	public static IEnumerator addScore (int score)
	{
		Dictionary<string, string> headers = new Dictionary<string, string> ();
		headers.Add ("Content-Type", "application/json");
		headers.Add ("Token", AppGlobal.token);

		ScoreRequest requestData = new ScoreRequest (score);
		String requestString = requestData.toJson ();

		var encoding = new System.Text.UTF8Encoding ();
		WWW request = new WWW (SCORE_URI, encoding.GetBytes (requestString), headers);
		yield return request;

		if (request.error != null) {
			Debug.Log ("request error: " + request.error);
		} else {
			Debug.Log ("request error: " + request.text);
		}
	}
}

