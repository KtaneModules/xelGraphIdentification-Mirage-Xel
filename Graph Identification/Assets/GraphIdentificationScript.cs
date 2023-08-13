using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

public class GraphIdentificationScript : MonoBehaviour {
    public KMBombModule BombHook;
    public KMAudio AudioHook;
        
    public Sprite[] GraphTextures;
    public SpriteRenderer[] SubmissionButtonRenderers;
    public TextMesh VertexDisplay;
    public TextMesh[] MovementButtonLabels;
    public KMSelectable[] MovementButtons;
    public KMSelectable[] SubmissionButtons;

    private List<List<List<int>>> _graphs = new List<List<List<int>>> {
    new List<List<int>> { new List<int> { 9, 1, 6 }, new List<int> { 0, 2, 7 }, new List<int> { 1, 3, 4 }, new List<int> { 2, 4, 8 }, new List<int> { 3, 5, 2 },
    new List<int> { 4, 6, 9 }, new List<int> { 5, 7, 0 }, new List<int> { 6, 8, 1 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 5 } },
    new List<List<int>> { new List<int> { 9, 1, 7 }, new List<int> { 0, 2, 8 }, new List<int> { 1, 3, 4 }, new List<int> { 2, 4, 5 }, new List<int> { 3, 5, 2 },
    new List<int> { 4, 6, 3 }, new List<int> { 5, 7, 9 }, new List<int> { 6, 8, 0 }, new List<int> { 7, 9, 1 }, new List<int> { 8, 0, 6 } },
    new List<List<int>> { new List<int> { 9, 1, 5 }, new List<int> { 0, 2, 6 }, new List<int> { 1, 3, 7 }, new List<int> { 2, 4, 8 }, new List<int> { 3, 5, 9 },
    new List<int> { 4, 6, 0 }, new List<int> { 5, 7, 1 }, new List<int> { 6, 8, 2 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 4 } },
    new List<List<int>> { new List<int> { 9, 1, 4 }, new List<int> { 0, 2, 7 }, new List<int> { 1, 3, 6 }, new List<int> { 2, 4, 8 }, new List<int> { 3, 5, 0 },
    new List<int> { 4, 6, 9 }, new List<int> { 5, 7, 2 }, new List<int> { 6, 8, 1 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 5 } },
    new List<List<int>> { new List<int> { 9, 1, 7 }, new List<int> { 0, 2, 5 }, new List<int> { 1, 3, 9 }, new List<int> { 2, 4, 8 }, new List<int> { 3, 5, 6 },
    new List<int> { 4, 6, 1 }, new List<int> { 5, 7, 4 }, new List<int> { 6, 8, 0 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 2 } },
    new List<List<int>> { new List<int> { 9, 1, 5 }, new List<int> { 0, 2, 6 }, new List<int> { 1, 3, 4 }, new List<int> { 2, 4, 8 }, new List<int> { 3, 5, 2 },
    new List<int> { 4, 6, 0 }, new List<int> { 5, 7, 2 }, new List<int> { 6, 8, 9 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 7 } },
    new List<List<int>> { new List<int> { 9, 1, 6 }, new List<int> { 0, 2, 5 }, new List<int> { 1, 3, 4 }, new List<int> { 2, 4, 8 }, new List<int> { 3, 5, 2 },
    new List<int> { 4, 6, 1 }, new List<int> { 5, 7, 0 }, new List<int> { 6, 8, 9 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 7 }  },
    new List<List<int>> { new List<int> { 9, 1, 6 }, new List<int> { 0, 2, 9 }, new List<int> { 1, 3, 4 }, new List<int> { 2, 4, 8 }, new List<int> { 3, 5, 2 },
    new List<int> { 4, 6, 7 }, new List<int> { 5, 7, 0 }, new List<int> { 6, 8, 5 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 1 } },
    new List<List<int>> { new List<int> { 9, 1, 2 }, new List<int> { 0, 2, 3 }, new List<int> { 1, 3, 0 }, new List<int> { 2, 9, 1 }, new List<int> { 8, 5, 6 },
    new List<int> { 4, 6, 7 }, new List<int> { 5, 7, 4 }, new List<int> { 6, 8, 5 }, new List<int> { 7, 9, 4 }, new List<int> { 8, 0, 3 } },
    new List<List<int>> { new List<int> { 9, 1, 2 }, new List<int> { 0, 2, 7 }, new List<int> { 1, 3, 0 }, new List<int> { 2, 4, 8 }, new List<int> { 3, 5, 6 },
    new List<int> { 4, 6, 9 }, new List<int> { 5, 7, 4 }, new List<int> { 6, 8, 1 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 5 } },
    new List<List<int>> { new List<int> { 9, 1, 2 }, new List<int> { 0, 2, 9 }, new List<int> { 1, 3, 0 }, new List<int> { 2, 4, 8 }, new List<int> { 3, 5, 6 },
    new List<int> { 4, 6, 7 }, new List<int> { 5, 7, 4 }, new List<int> { 6, 8, 5 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 1 } },
    new List<List<int>> { new List<int> { 9, 1, 6 }, new List<int> { 0, 2, 3 }, new List<int> { 1, 4, 7 }, new List<int> { 1, 5, 8 }, new List<int> { 2, 5, 9 },
    new List<int> { 4, 6, 3 }, new List<int> { 5, 7, 0 }, new List<int> { 6, 8, 2 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 4 } },
    new List<List<int>> { new List<int> { 9, 1, 6 }, new List<int> { 0, 2, 5 }, new List<int> { 1, 3, 9 }, new List<int> { 2, 4, 8 }, new List<int> { 3, 5, 7 },
    new List<int> { 4, 6, 1 }, new List<int> { 5, 7, 0 }, new List<int> { 6, 8, 4 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 2 } },
    new List<List<int>> { new List<int> { 9, 1, 7 }, new List<int> { 0, 2, 4 }, new List<int> { 1, 3, 5 }, new List<int> { 2, 4, 8 }, new List<int> { 3, 5, 1 },
    new List<int> { 4, 6, 2 }, new List<int> { 5, 7, 9 }, new List<int> { 6, 8, 0 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 6 } },
    new List<List<int>> { new List<int> { 9, 1, 8 }, new List<int> { 0, 2, 5 }, new List<int> { 1, 3, 4 }, new List<int> { 2, 4, 6 }, new List<int> { 3, 5, 2 },
    new List<int> { 4, 6, 1 }, new List<int> { 5, 7, 3 }, new List<int> { 6, 8, 9 }, new List<int> { 7, 9, 0 }, new List<int> { 8, 0, 7 } },
    new List<List<int>> { new List<int> { 9, 1, 7 }, new List<int> { 0, 2, 5 }, new List<int> { 1, 3, 4 }, new List<int> { 2, 4, 8 }, new List<int> { 3, 5, 2 },
    new List<int> { 4, 6, 1 }, new List<int> { 5, 7, 9 }, new List<int> { 6, 8, 0 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 6 } },
    new List<List<int>> { new List<int> { 9, 1, 8 }, new List<int> { 0, 2, 5 }, new List<int> { 1, 3, 4 }, new List<int> { 2, 4, 7 }, new List<int> { 3, 5, 2 },
    new List<int> { 4, 6, 1 }, new List<int> { 5, 7, 9 }, new List<int> { 6, 8, 3 }, new List<int> { 7, 9, 0 }, new List<int> { 8, 0, 6 } },
    new List<List<int>> { new List<int> { 9, 1, 6 }, new List<int> { 0, 2, 5 }, new List<int> { 1, 3, 7 }, new List<int> { 2, 4, 8 }, new List<int> { 3, 5, 9 },
    new List<int> { 4, 6, 1 }, new List<int> { 5, 7, 0 }, new List<int> { 6, 8, 2 }, new List<int> { 7, 9, 3 }, new List<int> { 8, 0, 4 } },
    };
    private List<List<int>> _moduleGraph;
    List<int> _potentialGraphIndices;
    private int _curVertex;
    private int _answer;
    private string _labels;

    int loggingId;
    static int loggingIdCounter = 1;
    private bool _solved;


	void Start () {
        loggingId = loggingIdCounter++;
		for (int i = 0; i < 5; i++)
        {
            int j = i;
            if (i < 3)
                MovementButtons[j].OnInteract += MovementPressHandler(j);
            SubmissionButtons[j].OnInteract += SubmissionPressHandler(j);
        }
        BombHook.OnActivate += delegate { PuzzleGenerator(); };
	}

    void PuzzleGenerator()
    {
         _potentialGraphIndices = Enumerable.Range(0, _graphs.Count()).ToList().Shuffle().Take(5).ToList();
        for (int i = 0; i < 5; i++)   
            SubmissionButtonRenderers[i].sprite = GraphTextures[_potentialGraphIndices[i]];
        _answer = UnityEngine.Random.Range(0, 5);
        _moduleGraph = _graphs[_potentialGraphIndices[_answer]];
        _labels = "abcdefghij".ToList().Shuffle().Join("");
        _curVertex = UnityEngine.Random.Range(0, 10);
        UpdateLabels();
       Debug.LogFormat("[Graph Identfication #{0}] The graph on the module is Graph {1}.", loggingId, _potentialGraphIndices[_answer] + 1);
       Debug.LogFormat("[Graph Identfication #{0}] Clockwise from top left, the vertices are labeled {1}.", loggingId, _labels.ToUpperInvariant());
    }

    void UpdateLabels()
    {
        VertexDisplay.text = _labels[_curVertex].ToString();
        for (int i = 0; i < 3; i++)
            MovementButtonLabels[i].text = _labels[_moduleGraph[_curVertex][i]].ToString();
    }

    KMSelectable.OnInteractHandler MovementPressHandler (int button) {
        return delegate
        {
            if (_solved)
                return false;
            AudioHook.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            MovementButtons[button].AddInteractionPunch(0.5f);
            _curVertex = _moduleGraph[_curVertex][button];
            UpdateLabels();
            return false;
        };

	}

    KMSelectable.OnInteractHandler SubmissionPressHandler(int button)
    {
        return delegate
        {
            if (_solved)
                return false;
            AudioHook.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            SubmissionButtons[button].AddInteractionPunch(0.5f);
            Debug.LogFormat("[Graph Identfication #{0}] You submitted Graph {1}.", loggingId, _potentialGraphIndices[button] + 1);
            if (button != _answer)
            {
                Debug.LogFormat("[Graph Identfication #{0}] That was incorrect. Strike!", loggingId);
                BombHook.HandleStrike();
                return false;
            }
            Debug.LogFormat("[Graph Identfication #{0}] That was correct. Module solved.", loggingId);
            BombHook.HandlePass();
            AudioHook.PlaySoundAtTransform("Solve", transform);
            _labels = "          ";
            UpdateLabels();
            for (int i = 0; i < 5; i++)
                SubmissionButtonRenderers[i].sprite = null;
            _solved = true;
            return false;
        };

    }
    string TwitchHelpMessage = "Use \"!{0} 1 | !{0} 1223\" to press those movement buttons in order (1 = Left). Use \"!{0} submit 5\" to submit that answer.";
    IEnumerator ProcessTwitchCommand(string command)
    {
        command = command.ToLowerInvariant().Trim();
        string[] cmds = command.Split(' ');
        if (cmds[0] == "submit")
        {
            if (cmds.Length > 2)
            {
                yield return "sendtochaterror Too many parameters!";
                yield break;
            }
            if (cmds.Length == 1)
            {
                yield return "sendtochaterror Specify a graph to submit!";
                yield break;
            }
            int submitNum;
            if (!int.TryParse(cmds[1], out submitNum))
            {
                yield return string.Format("sendtochaterror Unrecognized parameter \"{0}\"!", cmds[1]);
                yield break;
            }
            if (submitNum < 1 || submitNum > 5)
            {
                yield return string.Format("sendtochaterror Parameter \"{0}\" out of range!", cmds[1]);
                yield break;
            }
            yield return null;
            SubmissionButtons[submitNum - 1].OnInteract();
         }
        else
        {
            foreach (char i in command)
            {
                if(!"123, ".Contains(i))
                {
                    yield return string.Format("sendtochaterror Unrecognized parameter \"{0}\"!", i);
                    yield break;
                }
            } 
            yield return null;
            foreach (char i in command)
            {
                if (", ".Contains(i))
                {
                    continue;
                }
                yield return new WaitForSeconds(0.5f);
                MovementButtons[int.Parse(i.ToString()) - 1].OnInteract();
            }       
        }
    }
    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;
        SubmissionButtons[_answer].OnInteract();
    }
}
