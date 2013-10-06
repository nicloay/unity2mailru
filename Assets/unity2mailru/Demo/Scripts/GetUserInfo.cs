using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetUserInfo : MonoBehaviour {

	Rect windowRect = new Rect(280, 120, 120, 100);
	string text="";
	
    void OnGUI() {
        windowRect = GUILayout.Window(50, windowRect, DoMyWindow, "getUserName");
    }
	
   	void DoMyWindow(int windowID) {		
		
		if (GUILayout.Button("getUserInfo")){
			getUserInfo();
		}		
    }
	
	void getUserInfo(){
		
		MRUController.instance.callMailruByCallbackAndParams("mailru.common.friends.getExtended",delegate(object arg1, Callback arg2) {			
			List<object> result = arg1 as List<object>;
			List<object> paramsForGetInfoFunction = new List<object>();
			
			foreach(object user in result){
				Dictionary<string,object> userObject = user as Dictionary<string,object>;
				string uid = (string)userObject["uid"];
				Debug2.LogDebug("found uid = "+uid);
				paramsForGetInfoFunction.Add(uid);				
			}
			MRUController.instance.callMailruByCallbackAndParams("mailru.common.users.getInfo",delegate(object infoObj, Callback infoCallback) {
					
				Debug2.LogDebug("user info  ==== \n"+Json.Serialize(infoObj));				
			},paramsForGetInfoFunction);			
		},"",0);		
	}
	
	
}
