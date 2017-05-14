using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEnumerator : IEnumerator
{
    // Текущий объект, на который указывает итератор
    private Unit CurrentObj = null;

    /// <summary>
    /// Переопределить метод перехода к следующему элементу
    /// </summary>
    /// <returns>следующий юнит</returns>
    public bool MoveNext()
    {
        // Получить следующего юнита
        CurrentObj = (CurrentObj == null) ? Unit.FirstCreated : CurrentObj.NextUnit;

        // Вернуть следующего юнита
        return (CurrentObj != null);
    }

    /// <summary>
    /// Переустановить итератор на первого юнита
    /// </summary>
    public void Reset()
    {
        CurrentObj = null;
    }

    /// <summary>
    /// Свойство C# для доступа к текущему юниту
    /// </summary>
    public object Current
    {
        get { return CurrentObj; }
    }
}
