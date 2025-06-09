using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _delay;// задержка
    [SerializeField] private Text _text;// текст 
    [SerializeField] private KeyCode _keyCommandStartStopCoroutine;// задержка

    private int _numberText = 0; // счетчик
    private Coroutine _coroutine = null; // Контейнер для корутины

    // Функция обновления
    private void Update()
    {
        // При нажатии клавиши, запускаем или останавливаем корутину
        if (Input.GetKeyDown(_keyCommandStartStopCoroutine))
        {
            // Если корутина не запущена, запускаем
            if (_coroutine == null)
            {
                // Запускаем корутину с задержкой
                _coroutine = StartCoroutine(Scored(_delay));
            }
            // Если корутина не пуста, останавливаем
            else
            {
                // Останавливаем корутину
                StopCoroutine(_coroutine);
                // Обнуляем корутину
                _coroutine = null;
            }
        }
    }
    // Корутина
    private IEnumerator Scored(float delay)
    {
        // Ожидаем задержку
        WaitForSeconds wait = new WaitForSeconds(delay);
        // Пока не отключено, продолжаем работу
        while (enabled)
        {
            // Увеличиваем счетчик
            _numberText++;
            // Обновляем текст
            _text.text = Convert.ToString(_numberText);
            // Ожидаем задержку
            yield return wait;
        }
    }
}
