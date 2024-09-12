using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button MagicBox;
    public Button VerticalTunnel;
    public Button HorizontalTunnel;
    public Button Hyperbolic;

    void Awake()
    {
        // Prevent the GameObject this script is attached to from being destroyed on scene load
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        MagicBox.onClick.AddListener(MagicBoxClicked);
        VerticalTunnel.onClick.AddListener(VerticalTunnelClicked);
        HorizontalTunnel.onClick.AddListener(HorizontalTunnelClicked);
        Hyperbolic.onClick.AddListener(HyperbolicClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadScene("MagicBox");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadScene("TunnelDirection");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadScene("TunnelSize");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadScene("Hyperbolic");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    void MagicBoxClicked()
    {
        LoadScene("MagicBox");
    }

    void VerticalTunnelClicked()
    {
        LoadScene("TunnelDirection");
    }

    void HorizontalTunnelClicked()
    {
        LoadScene("TunnelSize");
    }

    void HyperbolicClicked()
    {
        LoadScene("Hyperbolic");
    }
}
