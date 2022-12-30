/**
* THIS SCRIPT IS GENERATED! DON'T MODIFY IT. 
*/

using UnityEngine;namespace Utils
{

public class ActionHandlerController : MonoBehaviour
{
public void InvokeOnToggleDialogContainer(){
ActionHandler.onToggleDialogContainer?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onToggleDialogContainer");
}
public void InvokeOnForegroundVisible(){
ActionHandler.onForegroundVisible?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onForegroundVisible");
}
public void InvokeOnPlayClick(){
ActionHandler.onPlayClick?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onPlayClick");
}
public void InvokeOnToggleSettings(){
ActionHandler.onToggleSettings?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onToggleSettings");
}
public void InvokeOnToggleItemsContainer(){
ActionHandler.onToggleItemsContainer?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onToggleItemsContainer");
}
public void InvokeOnOnlineAuthSuccess(){
ActionHandler.onOnlineAuthSuccess?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onOnlineAuthSuccess");
}
public void InvokeOnOnlineRegistration(){
ActionHandler.onOnlineRegistration?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onOnlineRegistration");
}
public void InvokeOnMoneyChange(int param){
ActionHandler.onMoneyChange?.Invoke(param);
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onMoneyChange");
}
public void InvokeOnMoneyIncrease(int param){
ActionHandler.onMoneyIncrease?.Invoke(param);
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onMoneyIncrease");
}
public void InvokeOnMoneyDecrease(int param){
ActionHandler.onMoneyDecrease?.Invoke(param);
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onMoneyDecrease");
}
public void InvokeOnDailyQuestChange(){
ActionHandler.onDailyQuestChange?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onDailyQuestChange");
}
public void InvokeOnUserLogin(){
ActionHandler.onUserLogin?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onUserLogin");
}
public void InvokeOnNewUser(){
ActionHandler.onNewUser?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onNewUser");
}
public void InvokeOnDeleteUser(){
ActionHandler.onDeleteUser?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onDeleteUser");
}
}
}