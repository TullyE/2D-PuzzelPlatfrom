using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Logic for inputs, and ground switch
//sound maybe added later to this script 12-19-21
public class GameLogic : MonoBehaviour
{
    public Ground redGround;
    public Ground blueGround;
    // public Ground grayGround;
    public Spikes redSpike;
    public Spikes blueSpike;
    List<Ground> grounds = new List<Ground>();
    //test
    //inputs
    Dictionary<KeyCode, string> specialInput = new Dictionary<KeyCode, string>()
    {
        {KeyCode.LeftArrow, "◄"},
        {KeyCode.RightArrow, "►"},
        {KeyCode.UpArrow, "▲"},
        {KeyCode.DownArrow, "▼"},
        {KeyCode.Mouse0, "lmb"},
        {KeyCode.Mouse1, "rmb"},
        {KeyCode.Mouse2, "M2"},
        {KeyCode.Mouse3, "M3"},
        {KeyCode.Mouse4, "M4"},
        {KeyCode.Mouse5, "M5"},
        {KeyCode.Mouse6, "M6"}
    };

        //UI texts
    public Text leftText;
    public Text rightText;
    public Text jumpText;
    public Text switchText;

        //UI buttons
    public Button switchButton;
    public Button rightButton;
    public Button leftButton;
    public Button jumpButton;
    public Button backButton;

        //default hotkeys
    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>()
    {
        {"left", KeyCode.A},
        {"right", KeyCode.D},
        {"jump", KeyCode.Space},
        {"switch", KeyCode.E},
        {"pause", KeyCode.Escape},
        {"kill", KeyCode.L}
    };

    private bool waitingForInput = false;

    //trail and particles
    public Trail trail;

    public LightParticles particles;

    //colors
    public Color pink = (new Color32(255, 63, 95, 255));

    public Color blue = (new Color32(61, 156, 255, 255));

    public GameObject PinkBG, PinkMid, BlueBG, BlueMid; //backgrounds

    public GameObject player;

    public GameObject pauseMenu;

    public Material lightbulbPink;

    public Material lightbulbBlue;

    private int groundNum = 1;

    private int totalGrounds = 2;

    public bool paused = false;

    void Start()
    {
        PinkBG.GetComponent<Renderer>().enabled = true;
        PinkMid.GetComponent<Renderer>().enabled = true;
        BlueBG.GetComponent<Renderer>().enabled = false;
        BlueMid.GetComponent<Renderer>().enabled = false;

        leftText.text = keys["left"].ToString().ToLower();
        rightText.text = keys["right"].ToString().ToLower();
        jumpText.text = keys["jump"].ToString().ToLower();
        switchText.text = keys["switch"].ToString().ToLower();

        grounds.Add(redGround);
        grounds.Add(blueGround);
        // grounds.Add(grayGround);
        grounds.Add(redSpike);
        grounds.Add(blueSpike);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keys["switch"]))
        {
            switchLvl();
        }

        if(Input.GetKeyDown(keys["pause"]))
        {
            pauseLogic();
        }
    }

    public void switchLvl()
    {
        if (Time.timeScale != 0)
        {
            if (this.groundNum == totalGrounds)
            {
                this.groundNum = 1;
                trail.setTrailColor (pink);
                particles.setParticleColor (pink);
                PinkBG.GetComponent<Renderer>().enabled = true;
                PinkMid.GetComponent<Renderer>().enabled = true;
                BlueBG.GetComponent<Renderer>().enabled = false;
                BlueMid.GetComponent<Renderer>().enabled = false;
                player.GetComponent<Renderer>().material = lightbulbPink;
                // return;
            }
            else
            {
                this.groundNum++;
                trail.setTrailColor (blue);
                particles.setParticleColor (blue);
                PinkBG.GetComponent<Renderer>().enabled = false;
                PinkMid.GetComponent<Renderer>().enabled = false;
                BlueBG.GetComponent<Renderer>().enabled = true;
                BlueMid.GetComponent<Renderer>().enabled = true;
                player.GetComponent<Renderer>().material = lightbulbBlue;
            }
            foreach(Ground g in grounds)
            {
                g.updateState();
            }
        }
    }

    public int getLvl()
    {
        return this.groundNum;
    }

    public void setLvl(int lvlNum)
    {
        this.groundNum = lvlNum;
    }

    public void pause()
    {
        paused = !paused;
        pauseMenu.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
    }

    public void quit()
    {
        Application.Quit();
    }

    public bool leftPressed()
    {
        return Input.GetKey(keys["left"]);
    }
    
    public bool rightPressed()
    {
        return Input.GetKey(keys["right"]);
    }
    
    public bool jumpPressed()
    {
        return Input.GetKey(keys["jump"]);
    }
    
    public bool switchPressed()
    {
        return Input.GetKey(keys["switch"]);
    }

    
    public IEnumerator updateHotkey(Text text, Button button, string key) // the 'C' at the end stands for coroutine
    {
        bool selection = Input.anyKeyDown;
        text.text = "-";
        text.color = new Color32(255, 255, 255, 128);
        enableDisableButtons(false);
        while (!selection)
        {
            selection = Input.anyKeyDown && !Input.GetKey(KeyCode.Escape);
            yield return null;

        }
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode))
            {
                string mt = null;
                if (specialInput.TryGetValue(kcode, out mt))
                {
                    text.text = mt;
                }
                else
                {
                    text.text = kcode.ToString().ToLower();
                }
                text.color = new Color32(255, 255, 255, 255);
                enableDisableButtons(true);
                keys[key] = kcode;
                yield return null;
            }
        }

    }
    public void updateSwitch()
    {
        StartCoroutine(updateHotkey(switchText, switchButton, "switch"));
    }
    public void updateJump()
    {
        StartCoroutine(updateHotkey(jumpText, jumpButton, "jump"));
    }
    public void updateLeft()
    {
        StartCoroutine(updateHotkey(leftText, leftButton, "left"));
    }
    public void updateRight()
    {
        StartCoroutine(updateHotkey(rightText, rightButton, "right"));
    }

    public void pauseLogic()
    {
        if(!paused)
        {
            pause();
        }
        else if(pauseMenu.activeSelf && !waitingForInput)
        {
            for(int i = 1; i < pauseMenu.transform.childCount; i ++)
            {
                if(pauseMenu.transform.GetChild(i).gameObject.activeSelf)//originalGameObject.transform.GetChild(0).gameObject;
                {
                    if(i == 1)
                    {
                        pauseMenu.SetActive(false);
                        pause();
                    }
                    else
                    {
                        pauseMenu.transform.GetChild(i).gameObject.SetActive(false);
                        pauseMenu.transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void enableDisableButtons(bool enabled)
    {
        switchButton.enabled = enabled;
        jumpButton.enabled = enabled;
        leftButton.enabled = enabled;
        rightButton.enabled = enabled;
        backButton.enabled = enabled;
        waitingForInput = !enabled;
    }
}
