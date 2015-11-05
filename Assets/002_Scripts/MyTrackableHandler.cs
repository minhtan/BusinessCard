﻿using UnityEngine;
using System.Collections;
using Vuforia;

public class MyTrackableHandler : MonoBehaviour,
                                                ITrackableEventHandler
{
    #region PRIVATE_MEMBER_VARIABLES

    private TrackableBehaviour mTrackableBehaviour;

    private bool mHasBeenFound = false;
    private bool mLostTracking;
    private float mSecondsSinceLost;
    private TargetControl tc;

    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNITY_MONOBEHAVIOUR_METHODS

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        tc = transform.GetChild(0).gameObject.GetComponent<TargetControl>();
        OnTrackingLost();
    }


    void Update()
    {
        // Pause the video if tracking is lost for more than two seconds
        if (mHasBeenFound && mLostTracking)
        {
            if (mSecondsSinceLost > 2.0f)
            {
                VideoPlaybackBehaviour video = GetComponentInChildren<VideoPlaybackBehaviour>();
                if (video != null &&
                    video.CurrentState == VideoPlayerHelper.MediaState.PLAYING)
                {
                    video.VideoPlayer.Pause();
                }

                mLostTracking = false;
            }

            mSecondsSinceLost += Time.deltaTime;
        }
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS



    #region PUBLIC_METHODS

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS



    #region PRIVATE_METHODS

    private void OnTrackingFound()
    {
        transform.GetChild(0).gameObject.SetActive(true);

        if (tc != null)
        {
            tc.Init();
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

        // Optionally play the video automatically when the target is found

        VideoPlaybackBehaviour video = GetComponentInChildren<VideoPlaybackBehaviour>();
        if (video != null && video.AutoPlay)
        {
            if (video.VideoPlayer.IsPlayableOnTexture())
            {
                VideoPlayerHelper.MediaState state = video.VideoPlayer.GetStatus();
                if (state == VideoPlayerHelper.MediaState.PAUSED ||
                    state == VideoPlayerHelper.MediaState.READY ||
                    state == VideoPlayerHelper.MediaState.STOPPED)
                {
                    // Pause other videos before playing this one
                    PauseOtherVideos(video);

                    // Play this video on texture where it left off
                    video.VideoPlayer.Play(false, video.VideoPlayer.GetCurrentPosition());
                }
                else if (state == VideoPlayerHelper.MediaState.REACHED_END)
                {
                    // Pause other videos before playing this one
                    PauseOtherVideos(video);

                    // Play this video from the beginning
                    video.VideoPlayer.Play(false, 0);
                }
            }
        }

        mHasBeenFound = true;
        mLostTracking = false;
    }


    private void OnTrackingLost()
    {
        if (tc != null) {
            tc.Reset();
        }
        
        transform.GetChild(0).gameObject.SetActive(false);

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");

        mLostTracking = true;
        mSecondsSinceLost = 0;
    }


    // Pause all videos except this one
    private void PauseOtherVideos(VideoPlaybackBehaviour currentVideo)
    {
        VideoPlaybackBehaviour[] videos = (VideoPlaybackBehaviour[])
                FindObjectsOfType(typeof(VideoPlaybackBehaviour));

        foreach (VideoPlaybackBehaviour video in videos)
        {
            if (video != currentVideo)
            {
                if (video.CurrentState == VideoPlayerHelper.MediaState.PLAYING)
                {
                    video.VideoPlayer.Pause();
                }
            }
        }
    }

    #endregion // PRIVATE_METHODS

}