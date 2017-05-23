using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Интерфейс ICommand для реализации в классах получателей событий
public interface ICommand 
{
    /// <summary>
    /// Устанавливает ссылку на подконтрольный юнит, которым будет упралять команда
    /// </summary>
    /// <param name="unit">подконтрольный юнит</param>
    void TakeUnit(Unit unit);
}
