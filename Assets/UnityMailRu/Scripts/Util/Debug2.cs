﻿using UnityEngine;
using System.Collections;
using System;

public enum LogLevel{
	DEBUG,
	INFO	
}

public class Debug2 {
	public static bool logUnityToJSConsole=true;
	public static LogLevel level = LogLevel.DEBUG;
	
	public static void Log(string str){
		log ("log",str,Debug.Log);
	}
	
	public static void LogError(string str){
		log ("error",str,Debug.LogError);
	}
	
	public static void LogWarning(string str){
		log("warn",str,Debug.LogWarning);
	}
	
	public static void LogDebug(string str){
		if (level==LogLevel.DEBUG)
			log ("debug",str, Debug.Log);
	}
	
	static void log(string logType, string body, Action<object> logAction){
		string finalString="UNITY["+Time.frameCount+"]: " + body;
		Debug.Log(finalString);
		string eval = @"
			console.LOG('STRING');
		".Replace("LOG",logType).Replace("STRING",finalString.Replace("'","\"").Replace(System.Environment.NewLine, "\\n") );		
		Debug.Log("eval = "+eval);
		Application.ExternalEval(eval);
	}
}
