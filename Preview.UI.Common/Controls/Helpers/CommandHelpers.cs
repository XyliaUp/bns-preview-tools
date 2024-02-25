using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Xylia.Preview.UI.Controls.Helpers;
internal static class CommandHelpers
{
    // Lots of specialized registration methods to avoid new'ing up more common stuff (like InputGesture's) at the callsite, as that's frequently
    // repeated and increases code size.  Do it once, here.  
    internal static void RegisterCommandHandler(this Type controlType, RoutedCommand command, ExecutedRoutedEventHandler executedRoutedEventHandler)
    {
        controlType.PrivateRegisterCommandHandler(command, executedRoutedEventHandler, null, null);
    }

    internal static void RegisterCommandHandler(this Type controlType, RoutedCommand command, ExecutedRoutedEventHandler executedRoutedEventHandler,
                                                InputGesture inputGesture)
    {
        controlType.PrivateRegisterCommandHandler(command, executedRoutedEventHandler, null, inputGesture);
    }

    internal static void RegisterCommandHandler(this Type controlType, RoutedCommand command, ExecutedRoutedEventHandler executedRoutedEventHandler,
                                                Key key)
    {
        controlType.PrivateRegisterCommandHandler(command, executedRoutedEventHandler, null, new KeyGesture(key));
    }

    internal static void RegisterCommandHandler(this Type controlType, RoutedCommand command, ExecutedRoutedEventHandler executedRoutedEventHandler,
                                                InputGesture inputGesture, InputGesture inputGesture2)
    {
        controlType.PrivateRegisterCommandHandler(command, executedRoutedEventHandler, null, inputGesture, inputGesture2);
    }

    internal static void RegisterCommandHandler(this Type controlType, RoutedCommand command, ExecutedRoutedEventHandler executedRoutedEventHandler,
                                                CanExecuteRoutedEventHandler canExecuteRoutedEventHandler)
    {
        controlType.PrivateRegisterCommandHandler(command, executedRoutedEventHandler, canExecuteRoutedEventHandler, null);
    }

    internal static void RegisterCommandHandler(this Type controlType, RoutedCommand command, ExecutedRoutedEventHandler executedRoutedEventHandler,
                                                CanExecuteRoutedEventHandler canExecuteRoutedEventHandler, InputGesture inputGesture)
    {
        controlType.PrivateRegisterCommandHandler(command, executedRoutedEventHandler, canExecuteRoutedEventHandler, inputGesture);
    }

    internal static void RegisterCommandHandler(this Type controlType, RoutedCommand command, ExecutedRoutedEventHandler executedRoutedEventHandler,
                                                CanExecuteRoutedEventHandler canExecuteRoutedEventHandler, Key key)
    {
        controlType.PrivateRegisterCommandHandler(command, executedRoutedEventHandler, canExecuteRoutedEventHandler, new KeyGesture(key));
    }

    internal static void RegisterCommandHandler(this Type controlType, RoutedCommand command, ExecutedRoutedEventHandler executedRoutedEventHandler,
                                                CanExecuteRoutedEventHandler canExecuteRoutedEventHandler, InputGesture inputGesture, InputGesture inputGesture2)
    {
        controlType.PrivateRegisterCommandHandler(command, executedRoutedEventHandler, canExecuteRoutedEventHandler, inputGesture, inputGesture2);
    }

    internal static void RegisterCommandHandler(this Type controlType, RoutedCommand command, ExecutedRoutedEventHandler executedRoutedEventHandler,
                                                CanExecuteRoutedEventHandler canExecuteRoutedEventHandler,
                                                InputGesture inputGesture, InputGesture inputGesture2, InputGesture inputGesture3, InputGesture inputGesture4)
    {
        controlType.PrivateRegisterCommandHandler(command, executedRoutedEventHandler, canExecuteRoutedEventHandler,
                                      inputGesture, inputGesture2, inputGesture3, inputGesture4);
    }

    internal static void RegisterCommandHandler(this Type controlType, RoutedCommand command, Key key, ModifierKeys modifierKeys,
                                                ExecutedRoutedEventHandler executedRoutedEventHandler, CanExecuteRoutedEventHandler canExecuteRoutedEventHandler)
    {
        controlType.PrivateRegisterCommandHandler(command, executedRoutedEventHandler, canExecuteRoutedEventHandler, new KeyGesture(key, modifierKeys));
    }

    // 'params' based method is private.  Call sites that use this bloat unwittingly due to implicit construction of the params array that goes into IL.
    private static void PrivateRegisterCommandHandler(this Type controlType, RoutedCommand command, ExecutedRoutedEventHandler executedRoutedEventHandler,
                                                      CanExecuteRoutedEventHandler canExecuteRoutedEventHandler, params InputGesture[] inputGestures)
    {
        // Validate parameters
        Debug.Assert(controlType != null);
        Debug.Assert(command != null);
        Debug.Assert(executedRoutedEventHandler != null);
        // All other parameters may be null

        // Create command link for this command
        CommandManager.RegisterClassCommandBinding(controlType, new CommandBinding(command, executedRoutedEventHandler, canExecuteRoutedEventHandler));

        // Create additional input binding for this command
        if (inputGestures != null)
        {
            for (int i = 0; i < inputGestures.Length; i++)
            {
                CommandManager.RegisterClassInputBinding(controlType, new InputBinding(command, inputGestures[i]));
            }
        }
    }
}