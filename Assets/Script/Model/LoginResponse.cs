using UnityEngine;
using System.Collections;

[System.Serializable]
public class LoginResponse : BaseResponse
{
	public string token;
	public string youtubeUrl;
	public string finalText;
	public string finalTextUrl;
}
