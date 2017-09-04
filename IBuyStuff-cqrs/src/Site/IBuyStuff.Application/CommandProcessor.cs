using System;
using System.Collections.Generic;
using IBuyStuff.Application.Commands;
using IBuyStuff.Application.Handlers;
using IBuyStuff.Application.ViewModels;

namespace IBuyStuff.Application
{
    public class CommandProcessor
    {
        private static readonly Dictionary<Type, Type> ListOfHandlers = new Dictionary<Type, Type>();

        public static void RegisterHandler<TCommand, TCommandHandler>()
            where TCommand : Command 
        {
            ListOfHandlers.Add(typeof(TCommand), typeof(TCommandHandler));
        }

        public static TViewModel Send<TCommand, TViewModel>(TCommand command)
            where TCommand : Command
            where TViewModel : ViewModelBase, new()
        {
            // Check if the message has a registered handler
            if (!ListOfHandlers.ContainsKey(typeof(TCommand))) 
                return new TViewModel();

            var typeOfHandler = ListOfHandlers[typeof(TCommand)];
            var instance = (ICommandHandler<TCommand, TViewModel>) Activator.CreateInstance(typeOfHandler);
            return instance.Handle(command);
        }
    }
}