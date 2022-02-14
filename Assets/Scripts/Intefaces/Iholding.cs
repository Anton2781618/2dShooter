using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//предмет который можно держать в руках
public interface Iholding
{
    //использовать
    void Use();

    //поднять
    void Take();

    //выбросить
    void Throw();

    //сдвигает редмет при прокрутке мышки
    void Sway();

    //спрятать предмет
    void Hide();

}
