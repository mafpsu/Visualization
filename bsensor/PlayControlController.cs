using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bsensor
{
    public class PlayControlController
    {
        private const string MODULE_TAG = "PlayControlController";

        private enum PlayState { Idle, PlayingForward, PlayingReverse, Paused };

        private PlayState playState = PlayState.Idle;

        #region EventPlayFinished
        public delegate void EventPlayFinished();
        EventPlayFinished eventPlayFinished;
        public void AddOnEventPlayFinished(EventPlayFinished e)
        {
            eventPlayFinished += e;
        }
        public void RemoveOnEventPlayFinished(EventPlayFinished e)
        {
            eventPlayFinished -= e;
        }
        public void OnEventPlayFinished()
        {
            if (null != eventPlayFinished)
                eventPlayFinished();
        }
        #endregion EventPlayFinished

        #region EventTimerTick
        public delegate void EventTimerTick(int markerIndex, bool isForward);
        EventTimerTick eventTimerTick;
        public void AddOnEventTimerTick(EventTimerTick e)
        {
            eventTimerTick += e;
        }
        public void RemoveOnEventTimerTick(EventTimerTick e)
        {
            eventTimerTick -= e;
        }
        public void OnEventTimerTick(int markerIndex, bool isForward)
        {
            if (null != eventTimerTick)
                eventTimerTick(markerIndex, isForward);
        }
        #endregion EventTimerTick

        #region EventClearMarkers
        public delegate void EventClearMarkers();
        EventClearMarkers eventClearMarkers;
        public void AddOnEventClearMarkers(EventClearMarkers e)
        {
            eventClearMarkers += e;
        }
        public void RemoveOnEventClearMarkers(EventClearMarkers e)
        {
            eventClearMarkers -= e;
        }
        public void OnEventClearMarkers()
        {
            if (null != eventClearMarkers)
                eventClearMarkers();
        }
        #endregion EventClearMarkers


        private int? playMarkerIndex = null;
        private int numPoints;

        private Button btnPlayPauseForward;
        private Button btnPlayPauseReverse;
        private Button btnStepForward;
        private Button btnStepReverse;
        private Button btnStop;
        private Timer tmrPlay;
        private bool stopAfterOneTick;

        public PlayControlController(Button btnPlayPauseForward,
            Button btnPlayPauseReverse,
            Button btnStepForward,
            Button btnStepReverse,
            Button btnStop,
            Timer tmrPlay)
        {
        this.btnPlayPauseForward = btnPlayPauseForward;
        this.btnPlayPauseReverse = btnPlayPauseReverse;
        this.btnStepForward = btnStepForward;
        this.btnStepReverse = btnStepReverse;
        this.btnStop = btnStop;
        this.tmrPlay = tmrPlay;
            this.tmrPlay.Tick += new System.EventHandler(this.tmrPlay_Tick);
            SetPlayState(null);
        }

        public StopLevelData StopLevelData
        {
            set { numPoints = value.LatLngs.Count; }
        }

        public void PlayPauseForward()
        {
            if (numPoints > 0)
            {
                SetPlayState(btnPlayPauseForward);
            }
        }

        public void PlayPauseReverse()
        {
            if (numPoints > 0)
            {
                SetPlayState(btnPlayPauseReverse);
            }
        }

        public void StepForward()
        {
            if (numPoints > 0)
            {
                SetPlayState(btnStepForward);
            }
        }

        public void StepReverse()
        {
            if (numPoints > 0)
            {
                SetPlayState(btnStepReverse);
            }
        }

        public void Stop()
        {
            SetPlayState(btnStop);
            OnEventPlayFinished();
        }

        private void Tick()
        {
            if (playState == PlayState.PlayingForward)
            {
                if (null == playMarkerIndex)
                {
                    OnEventClearMarkers();
                    playMarkerIndex = 0;
                }
                else
                {
                    ++playMarkerIndex;
                }

                if (playMarkerIndex >= numPoints)
                {
                    OnEventPlayFinished();
                    playState = PlayState.Idle;
                    playMarkerIndex = null;
                }
                else
                {
                    OnEventTimerTick((int)playMarkerIndex, true);
                }
            }
            else if (playState == PlayState.PlayingReverse)
            {
                if (playMarkerIndex != null)
                {
                    OnEventTimerTick((int)playMarkerIndex, false);
                    playMarkerIndex = playMarkerIndex > 0 ? playMarkerIndex - 1 : null;
                    if (playMarkerIndex == null)
                    {
                        OnEventPlayFinished();
                        playState = PlayState.Idle;
                    }
                }
            }
        }

        private void tmrPlay_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.numPoints > 0)
                {
                    Tick();
                }
                if (playState == PlayState.Idle)
                {
                    tmrPlay.Enabled = false;
                    SetPlayState(null);
                }
                else if (stopAfterOneTick)
                {
                    SetUiStatePaused();
                    playState = PlayState.Paused;
                    tmrPlay.Enabled = false;
                }
                else
                {
                    tmrPlay.Interval = 1000;
                }
            }
            catch (Exception ex)
            {
                Util.LogException(MODULE_TAG, ex);
            }
        }

        private void SetPlayState(Button button)
        {
            if (null == button)
            {
                btnPlayPauseForward.Enabled = true;
                btnPlayPauseForward.Text = ">>>";
                btnPlayPauseReverse.Enabled = false;
                btnPlayPauseReverse.Text = "<<<";
                btnStepForward.Enabled = true;
                btnStepReverse.Enabled = false;
                btnStop.Enabled = false;
                btnStop.Text = "Stop";

                playState = PlayState.Idle;
                playMarkerIndex = null;
            }
            else
            {
                switch (playState)
                {
                    case PlayState.Idle:

                        if (button == btnPlayPauseForward || button == btnStepForward)
                        {
                            //playMarkerIndex = 0;
                            playState = PlayState.PlayingForward;
                            stopAfterOneTick = (button == btnStepForward);
                        }
                        break;

                    case PlayState.PlayingForward:

                        if (button == btnPlayPauseForward) // actually Pause
                        {
                            playState = PlayState.Paused;
                        }
                        else if (button == btnStop)
                        {
                            playState = PlayState.Idle;
                            playMarkerIndex = null;
                        }
                        stopAfterOneTick = false;
                        break;

                    case PlayState.PlayingReverse:

                        if (button == btnPlayPauseReverse) // actually Pause
                        {
                            playState = PlayState.Paused;

                        }
                        else if (button == btnStop)
                        {
                            playState = PlayState.Idle;
                            playMarkerIndex = null;
                        }
                        stopAfterOneTick = false;
                        break;

                    case PlayState.Paused:

                        if (button == btnPlayPauseForward)
                        {
                            playState = PlayState.PlayingForward;
                            stopAfterOneTick = false;
                        }
                        else if (button == btnStepForward)
                        {
                            playState = PlayState.PlayingForward;
                            stopAfterOneTick = true;
                            tmrPlay.Interval = 1;
                        }
                        else if (button == btnPlayPauseReverse)
                        {
                            playState = PlayState.PlayingReverse;
                            stopAfterOneTick = false;
                            // We must increment the index because when the next tick occurs,
                            // the index will be decremented before the point is removed
                            //++playMarkerIndex;
                        }
                        else if (button == btnStepReverse)
                        {
                            playState = PlayState.PlayingReverse;
                            stopAfterOneTick = true;
                            tmrPlay.Interval = 1;
                            // We must increment the index because when the next tick occurs,
                            // the index will be decremented before the point is removed
                            //++playMarkerIndex;
                        }
                        else if (button == btnStop)
                        {
                            playState = PlayState.Idle;
                            stopAfterOneTick = true;
                        }
                        break;
                }

                // Set buttons according to new play state
                switch (playState)
                {
                    case PlayState.Idle:

                        btnPlayPauseForward.Text = ">>>";
                        btnPlayPauseForward.Enabled = true;
                        btnPlayPauseReverse.Text = "<<<";
                        btnPlayPauseReverse.Enabled = false;
                        btnStepForward.Enabled = true;
                        btnStepReverse.Enabled = false;
                        btnStop.Enabled = false;
                        break;

                    case PlayState.PlayingForward:

                        btnPlayPauseForward.Text = "Pause";
                        btnPlayPauseForward.Enabled = true;
                        btnPlayPauseReverse.Enabled = false;
                        btnStepForward.Enabled = false;
                        btnStepReverse.Enabled = false;
                        btnStop.Enabled = true;
                        break;

                    case PlayState.PlayingReverse:

                        btnPlayPauseForward.Enabled = false;
                        btnPlayPauseReverse.Text = "Pause";
                        btnPlayPauseReverse.Enabled = true;
                        btnStepForward.Enabled = false;
                        btnStepReverse.Enabled = false;
                        btnStop.Enabled = true;
                        break;

                    case PlayState.Paused:

                        SetUiStatePaused();
                        break;
                }
            }
            tmrPlay.Enabled = IsPlaying;
        }

        private void SetUiStatePaused()
        {
            btnPlayPauseForward.Text = ">>>";
            btnPlayPauseForward.Enabled = true;
            btnPlayPauseReverse.Text = "<<<";
            btnPlayPauseReverse.Enabled = true;
            btnStepForward.Enabled = true;
            btnStepReverse.Enabled = true;
            btnStop.Enabled = true;
        }

        public bool IsPlaying
        {
            get { return ((playState == PlayState.PlayingForward) || (playState == PlayState.PlayingReverse)); }
        }
    }
}
