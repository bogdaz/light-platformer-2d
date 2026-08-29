using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages game localization (Ukrainian, Russian, English)
/// </summary>
public class LocalizationManager : MonoBehaviour
{
    private static LocalizationManager _instance;
    private SystemLanguage _currentLanguage = SystemLanguage.English;
    
    private Dictionary<string, Dictionary<SystemLanguage, string>> _localizationData;
    
    public static LocalizationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LocalizationManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("LocalizationManager");
                    _instance = go.AddComponent<LocalizationManager>();
                }
            }
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        LoadLocalizationData();
        LoadSavedLanguage();
    }
    
    private void LoadLocalizationData()
    {
        _localizationData = new Dictionary<string, Dictionary<SystemLanguage, string>>
        {
            // Main Menu
            { "btn_play", new Dictionary<SystemLanguage, string>
            {
                { SystemLanguage.Ukrainian, "Грати" },
                { SystemLanguage.Russian, "Играть" },
                { SystemLanguage.English, "Play" }
            }},
            { "btn_records", new Dictionary<SystemLanguage, string>
            {
                { SystemLanguage.Ukrainian, "Рекорди" },
                { SystemLanguage.Russian, "Рекорды" },
                { SystemLanguage.English, "Records" }
            }},
            { "btn_settings", new Dictionary<SystemLanguage, string>
            {
                { SystemLanguage.Ukrainian, "Налаштування" },
                { SystemLanguage.Russian, "Настройки" },
                { SystemLanguage.English, "Settings" }
            }},
            { "btn_quit", new Dictionary<SystemLanguage, string>
            {
                { SystemLanguage.Ukrainian, "Вийти" },
                { SystemLanguage.Russian, "Выход" },
                { SystemLanguage.English, "Quit" }
            }},
            
            // Game UI
            { "label_energy", new Dictionary<SystemLanguage, string>
            {
                { SystemLanguage.Ukrainian, "Енергія" },
                { SystemLanguage.Russian, "Энергия" },
                { SystemLanguage.English, "Energy" }
            }},
            { "label_time", new Dictionary<SystemLanguage, string>
            {
                { SystemLanguage.Ukrainian, "Час" },
                { SystemLanguage.Russian, "Время" },
                { SystemLanguage.English, "Time" }
            }},
            { "label_level", new Dictionary<SystemLanguage, string>
            {
                { SystemLanguage.Ukrainian, "Рівень" },
                { SystemLanguage.Russian, "Уровень" },
                { SystemLanguage.English, "Level" }
            }},
            
            // Game Over
            { "game_over", new Dictionary<SystemLanguage, string>
            {
                { SystemLanguage.Ukrainian, "Гра Закінчена" },
                { SystemLanguage.Russian, "Игра Окончена" },
                { SystemLanguage.English, "Game Over" }
            }},
            { "victory", new Dictionary<SystemLanguage, string>
            {
                { SystemLanguage.Ukrainian, "Перемога!" },
                { SystemLanguage.Russian, "Победа!" },
                { SystemLanguage.English, "Victory!" }
            }},
            { "btn_retry", new Dictionary<SystemLanguage, string>
            {
                { SystemLanguage.Ukrainian, "Спробувати ще раз" },
                { SystemLanguage.Russian, "Попробовать снова" },
                { SystemLanguage.English, "Try Again" }
            }},
            { "btn_menu", new Dictionary<SystemLanguage, string>
            {
                { SystemLanguage.Ukrainian, "На Головне Меню" },
                { SystemLanguage.Russian, "На Главное Меню" },
                { SystemLanguage.English, "Main Menu" }
            }},
        };
    }
    
    public void SetLanguage(SystemLanguage language)
    {
        _currentLanguage = language;
        PlayerPrefs.SetInt("GameLanguage", (int)language);
        PlayerPrefs.Save();
    }
    
    private void LoadSavedLanguage()
    {
        if (PlayerPrefs.HasKey("GameLanguage"))
        {
            _currentLanguage = (SystemLanguage)PlayerPrefs.GetInt("GameLanguage");
        }
        else
        {
            _currentLanguage = Application.systemLanguage;
            if (_currentLanguage != SystemLanguage.Ukrainian && 
                _currentLanguage != SystemLanguage.Russian && 
                _currentLanguage != SystemLanguage.English)
            {
                _currentLanguage = SystemLanguage.English;
            }
        }
    }
    
    public string GetText(string key)
    {
        if (_localizationData.ContainsKey(key))
        {
            var translations = _localizationData[key];
            if (translations.ContainsKey(_currentLanguage))
            {
                return translations[_currentLanguage];
            }
            // Fallback to English
            return translations[SystemLanguage.English];
        }
        
        return key; // Return key if not found
    }
    
    public SystemLanguage GetCurrentLanguage()
    {
        return _currentLanguage;
    }
}
