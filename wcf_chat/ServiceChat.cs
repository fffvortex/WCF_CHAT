using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace wcf_chat
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // атрибут для раюоты сервера в одной сессии
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;
        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                Id = nextId,
                Name = name,
                operation = OperationContext.Current,
            };
            nextId++;
            SendMessage($"{user.Name} is connected!", 0);
            users.Add(user);
            return user.Id;

        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                users.Remove(user);
                SendMessage($"{user.Name} is disconected.", 0);
            }
        }

        public void SendMessage(string message, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    answer += ": " + user.Name + " ";
                }
                answer += message;

                item.operation.GetCallbackChannel<IServerCallBack>().MessageCallBack(answer);
            }

        }
    }
}
