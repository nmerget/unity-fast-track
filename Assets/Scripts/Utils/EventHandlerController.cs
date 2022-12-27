/**
* THIS SCRIPT IS GENERATED! DON'T MODIFY IT. 
*/

using UnityEngine;

public class EventHandlerController : MonoBehaviour
{
public void InvokeOnToogleDialogContainer(){
EventHandler.onToogleDialogContainer?.Invoke();
}
public void InvokeOnForgroundVisible(){
EventHandler.onForgroundVisible?.Invoke();
}
public void InvokeOnPlayClick(){
EventHandler.onPlayClick?.Invoke();
}
public void InvokeOnToggleSettings(){
EventHandler.onToggleSettings?.Invoke();
}
public void InvokeOnToggleItemsContainer(){
EventHandler.onToggleItemsContainer?.Invoke();
}
public void InvokeOnOnlineAuthSuccess(){
EventHandler.onOnlineAuthSuccess?.Invoke();
}
public void InvokeOnOnlineRegistration(){
EventHandler.onOnlineRegistration?.Invoke();
}
public void InvokeOnMoneyChange(int param){
EventHandler.onMoneyChange?.Invoke(param);
}
public void InvokeOnMoneyIncrease(int param){
EventHandler.onMoneyIncrease?.Invoke(param);
}
public void InvokeOnMoneyDecrease(int param){
EventHandler.onMoneyDecrease?.Invoke(param);
}
public void InvokeOnDailyQuestChange(){
EventHandler.onDailyQuestChange?.Invoke();
}
public void InvokeOnPlayerLogin(){
EventHandler.onPlayerLogin?.Invoke();
}
public void InvokeOnNewPlayer(){
EventHandler.onNewPlayer?.Invoke();
}
}