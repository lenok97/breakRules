using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Синглтон EventManager для отправки событий получателям
//Работает с реализациями IListener
public class EventManager : MonoBehaviour
{
#region свойства C#
    //Общий доступ к экземпляру
    public static EventManager Instance
    {
        get { return instance; }
        set { }
    }
#endregion

#region переменные
    //Экземпляр диспечера событий (синглтон)
    private static EventManager instance = null;

    // Тип делегата, обрабатывающего события
    public delegate void OnEvent(EVENT_TYPE Event_Type,
        Component Sender, object Param = null);

    //Массив получателей (все зарегистрировавшиеся обьекты)
    private Dictionary<EVENT_TYPE, List<OnEvent>> Listeners =
        new Dictionary<EVENT_TYPE, List<OnEvent>>();
    #endregion
#region методы
    // Вызывается перед началом работы для инициализации
    private void Awake()
    {
        // Если экземпляр отсутствует, сохранить данный экземпляр
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
    }

    /// <summary>
    /// Функция добавления получателя в массив
    /// </summary>
    /// <param name="Event_Type">Событие, ожидаемое получателем</param>
    /// <param name="Listener">Объект, ожидающий события</param>
    public void AddListener(EVENT_TYPE Event_Type, OnEvent Listener)
    {
        // Список получателей для данного события
        List<OnEvent> ListenList = null;

        // Проверить тип события. Если существует - добавить в список
        if(Listeners.TryGetValue(Event_Type, out ListenList))
        {
            // Список существует, добавить новый элемент
            ListenList.Add(Listener);
            return;
        }

        // Иначе создать список как ключ словаря
        ListenList = new List<OnEvent>();
        ListenList.Add(Listener);
        Listeners.Add(Event_Type, ListenList);
    }

    /// <summary>
    /// Посылает события получателям
    /// </summary>
    /// <param name="Event_Type">Событие для вызова</param>
    /// <param name="Sender">Вызываемый объект</param>
    /// <param name="Param">Необязательный аргумент</param>
    public void PostNotification (EVENT_TYPE Event_Type, Component Sender = null, object Param = null)
    {
        // Посылать событие всем получателям
        // Список получателей только для данного события
        List<OnEvent> ListenList = null;

        // Если получателя нет - выйти
        if (!Listeners.TryGetValue(Event_Type, out ListenList))
            return;

        // Получатели есть. Послать им событие
        for(int i = 0; i<ListenList.Count; i++)
        {
            if (!ListenList[i].Equals(null))
                ListenList[i](Event_Type, Sender, Param);
        }
    }

    // Удаляет событие из словаря, включая всех получателей
    public void RemoveEvent(EVENT_TYPE Event_Type)
    {
        // Удалить запись из словаря
        Listeners.Remove(Event_Type);
    }

    // Удалить все избыточные записи из словаря
    public void RemoveRedundancies()
    {
        // Создать новый словарь
        Dictionary<EVENT_TYPE, List<OnEvent>> TmpListeners =
            new Dictionary<EVENT_TYPE, List<OnEvent>>();

        // Обойти все записи в словаре 
        foreach(KeyValuePair<EVENT_TYPE, List<OnEvent>> Item in Listeners)
        {
            // Обойти всех получателей,удалить пустые ссылки
            for(int i = Item.Value.Count-1; i>=0; i--)
            {
                // Если ссылка пустая, удалить элемент
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            }

            // Если в списке остались элементы, добавить его в словарь tmp
            if (Item.Value.Count > 0)
                TmpListeners.Add(Item.Key, Item.Value);
        }

        //Заменить обьект Listeners новым словарем
        Listeners = TmpListeners;
    }
    
    //Вызывается при смене сцены. Очищает словарь
    private void OnLevelWasLoaded(int level)
    {
        //RemoveRedundancies();
        Listeners.Clear();
    }
#endregion
}
