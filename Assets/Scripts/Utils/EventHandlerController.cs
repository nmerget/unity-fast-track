/**
* THIS SCRIPT IS GENERATED! DON'T MODIFY IT. 
*/

using UnityEngine;
namespace Utils
{

public class EventHandlerController : MonoBehaviour
{
public void InvokeOnToggleDialogContainer(){
EventHandler.onToggleDialogContainer?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onToggleDialogContainer");
}
public void InvokeOnForegroundVisible(){
EventHandler.onForegroundVisible?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onForegroundVisible");
}
public void InvokeOnPlayClick(){
EventHandler.onPlayClick?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onPlayClick");
}
public void InvokeOnToggleSettings(){
EventHandler.onToggleSettings?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onToggleSettings");
}
public void InvokeOnToggleItemsContainer(){
EventHandler.onToggleItemsContainer?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onToggleItemsContainer");
}
public void InvokeOnOnlineAuthSuccess(){
EventHandler.onOnlineAuthSuccess?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onOnlineAuthSuccess");
}
public void InvokeOnOnlineRegistration(){
EventHandler.onOnlineRegistration?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onOnlineRegistration");
}
public void InvokeOnMoneyChange(int param){
EventHandler.onMoneyChange?.Invoke(param);
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onMoneyChange");
}
public void InvokeOnMoneyIncrease(int param){
EventHandler.onMoneyIncrease?.Invoke(param);
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onMoneyIncrease");
}
public void InvokeOnMoneyDecrease(int param){
EventHandler.onMoneyDecrease?.Invoke(param);
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onMoneyDecrease");
}
public void InvokeOnDailyQuestChange(){
EventHandler.onDailyQuestChange?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onDailyQuestChange");
}
public void InvokeOnPlayerLogin(){
EventHandler.onPlayerLogin?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onPlayerLogin");
}
public void InvokeOnNewPlayer(){
EventHandler.onNewPlayer?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onNewPlayer");
}
public void InvokeOnDeletePlayer(){
EventHandler.onDeletePlayer?.Invoke();
if(DebugManager.instance.debug && DebugManager.instance.logEvents) Debug.Log("Invoked event: onDeletePlayer");
}
}
}