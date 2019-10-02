using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] //Для всех клиентов создается один единый сервис
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;

        public int Connect(string name)
        {
            ServerUser user = new ServerUser(){
                ID = nextId,
                Name = name,
                operationcontext = OperationContext.Current

            };
            nextId ++;
            SendMsg(" " + user.Name + " подключился к чату!", 0);
            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id); //поиск пользователя по id
            if (user != null)
            {
                users.Remove(user);
                SendMsg(" " + user.Name + "покинул чат!", 0);
            }
        }

        public void SendMsg(string msg, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(i => i.ID == id); //поиск пользователя по id
                if (user != null)
                {
                    answer += "  " + user.Name + "   ";
                }
                answer += msg;
                item.operationcontext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer);
            }
        }
    }
}
