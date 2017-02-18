using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefs : MonoBehaviour {

    public Color PlayerColor;

    public string PlayerName;

    public void Start() {
        var arrow = transform.FindChild("Head").FindChild("arrow");
        var tailRenderer = transform.FindChild("Tail").GetComponent<LineRenderer>();
        GameObject.Find("Score" + PlayerName).GetComponent<Text>().color = PlayerColor;
        var pManager = GameObject.Find("_PlayerManager").GetComponent<PlayerManager>();
        tailRenderer.startColor = PlayerColor;
        tailRenderer.endColor = PlayerColor;
        tailRenderer.colorGradient = CreateGradient(PlayerColor, PlayerColor);
        if (arrow) {
            arrow.GetComponent<SpriteRenderer>().color = PlayerColor;
            var lText = arrow.FindChild("TXT_DIRECTIONS_L").GetComponent<TextMesh>();
            var rText = arrow.FindChild("TXT_DIRECTIONS_R").GetComponent<TextMesh>();
            lText.color = PlayerColor;
            rText.color = PlayerColor;
            lText.text = pManager.PlayersInputs[GetPlayerNumber() - 1].LeftKey;
            rText.text = pManager.PlayersInputs[GetPlayerNumber() - 1].RightKey;
        }
    }

    public int GetPlayerNumber() {
        return Int32.Parse(Regex.Match(PlayerName, @"\d+").Value);
    }

    private Gradient CreateGradient(Color firstColor, Color secondColor) {
        var g = new Gradient();
        var gck = new GradientColorKey[2];
        gck[0].color = firstColor;
        gck[0].time = 0.0F;
        gck[1].color = secondColor;
        gck[1].time = 1.0F;
        var gak = new GradientAlphaKey[2];
        gak[0].alpha = 1.0F;
        gak[0].time = 0.0F;
        gak[1].alpha = 1.0F;
        gak[1].time = 1.0F;
        g.SetKeys(gck, gak);
        return g;
    }
}
