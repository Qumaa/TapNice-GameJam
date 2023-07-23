﻿using Project.UI.Animation;
using TMPro;
using UnityEngine.UI;

namespace Project.UI
{
    public class GameplayUIFader : UIFader
    {
        private readonly Button _pauseButton;
        private readonly TextMeshProUGUI _timeLabel;
        private readonly TextMeshProUGUI _bestTimeLabel;

        public GameplayUIFader(Button pauseButton, TextMeshProUGUI timeLabel, TextMeshProUGUI bestTimeLabel)
        {
            _pauseButton = pauseButton;
            _timeLabel = timeLabel;
            _bestTimeLabel = bestTimeLabel;
        }

        public override void SetFade(float fade)
        {
            base.SetFade(fade);
            
            SetButtonFade(fade);
            SetScoreFade(fade);
        }

        private void SetButtonFade(float fade)
        {
            var color = _pauseButton.image.color;
            color.a = fade;
            _pauseButton.image.color = color;
        }

        private void SetScoreFade(float fade)
        {
            SetTextFade(_timeLabel, fade);
            SetTextFade(_bestTimeLabel, fade);
        }

        private static void SetTextFade(TextMeshProUGUI text, float fade)
        {
            var color = text.color;
            color.a = fade;
            text.color = color;
        }
    }
}