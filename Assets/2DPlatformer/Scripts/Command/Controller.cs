using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //Общий доступ к экземпляру
    public static Controller Instance
    {
        get { return instance; }
        set { }
    }
    //Экземпляр контроллера
    private static Controller instance = null;
    
    private List<ICommand> commands = new List<ICommand>();
    Unit unit = null;
    
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

    private void Start()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.CHENGE_UNIT, this.ChangeUnit);
    }

    /// <summary>?
    /// Функция добавления команды управляющей юнитом
    /// </summary>
    /// <param name="command">Обьект, управляющий юнитом</param>
    public void AddCommand(ICommand command)
    {
        commands.Add(command);
        command.TakeUnit(Unit.FirstCreated);
    }

    /// <summary>
    /// Сменяет подуправляемого юнита
    /// </summary>
    public void ChangeUnit(EVENT_TYPE Event_Type, Component sender, object param)
    {
        if (unit == null)
            unit = Unit.FirstCreated;
        if (unit.NextUnit == null) ChangeOnFirsUnit();
        else
            if ((unit.transform.position - unit.NextUnit.transform.position).magnitude < (float)param)
        {
            unit = unit.NextUnit;
            ChangeOnUnit(unit);
        }
        else ChangeOnFirsUnit();
        
    }

    private void ChangeOnUnit(Unit unit)
    {
        foreach (ICommand command in commands)
            if (!command.Equals(null))
            {
                command.TakeUnit(unit);
            }
    }

    /// <summary>
    /// Делает подуправляемым первового юнита
    /// </summary>
    public void ChangeOnFirsUnit()
    {
        unit = Unit.FirstCreated;
        foreach (ICommand command in commands)
            if (!command.Equals(null))
            {
                command.TakeUnit(unit);
            }
        
    }

    /// <summary>
    /// Удаляет юнита из списка подуправляемых юнитов
    /// </summary>
    /// <param name="delateUnit">Удаляемый юнит</param>
    public void RemoveUnit(Unit delateUnit)
    {
        if (unit == null)
            unit = Unit.FirstCreated;
        if (delateUnit.GetInstanceID() == this.unit.GetInstanceID())
            ChangeOnFirsUnit();  
    }
    void OnLevelWasLoaded(int level)
    {
        commands.Clear();
    }
}
