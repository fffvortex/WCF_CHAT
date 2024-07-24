
using System.ServiceModel;

namespace wcf_chat
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract(CallbackContract =typeof(IServerCallBack))]
    public interface IServiceChat
    {
        [OperationContract] // с помощью этого атрибута клиентская часть видит серверные методы
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)] // параметр атрибута указывает что ответ от сервера ожидать не нужно
        void SendMessage(string message, int id);

    }
    public interface IServerCallBack // функция обратного вызова на стороне клиента
    {
        [OperationContract(IsOneWay = true)]
        void MessageCallBack(string message);
    }

}
