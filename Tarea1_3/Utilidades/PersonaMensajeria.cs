using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Tarea1_3.Utilidades
{
    public  class PersonaMensajeria : ValueChangedMessage<PersonaMensaje>
    {
        public PersonaMensajeria(PersonaMensaje value) : base(value)
        {
            
        }
    }
}
