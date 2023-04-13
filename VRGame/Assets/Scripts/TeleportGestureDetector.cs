using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Hands;

public class TeleportGestureDetector : MonoBehaviour
{
    private static readonly List<XRHandSubsystem> Subsystems = new();

    [SerializeField] private Handedness handedness;
    [SerializeField] private UnityEvent showTeleport;
    [SerializeField] private UnityEvent hideTeleport;
    [SerializeField] private UnityEvent activateTeleport;
    private XRHandSubsystem handSubsystem;
    private bool isPoking;

    private void OnEnable()
    {
        SubsystemManager.GetSubsystems(Subsystems);
        if (Subsystems.Count == 0)
            return;

        handSubsystem = Subsystems[0];
        handSubsystem.updatedHands += OnHandsUpdated;
        handSubsystem.trackingLost += OnHandTrackingLost;
    }

    private void OnDisable()
    {
        if (handSubsystem == null)
            return;

        handSubsystem.updatedHands -= OnHandsUpdated;
        handSubsystem.trackingLost -= OnHandTrackingLost;
        handSubsystem = null;
    }

    private void OnHandsUpdated(XRHandSubsystem _, XRHandSubsystem.UpdateSuccessFlags updateSuccessFlags, XRHandSubsystem.UpdateType updateType)
    {
        var wasPoking = isPoking;
        switch (handedness)
        {
            case Handedness.Left:
                if (!HasUpdateSuccessFlag(updateSuccessFlags, XRHandSubsystem.UpdateSuccessFlags.LeftHandJoints))
                    return;

                var leftHand = handSubsystem.leftHand;
                isPoking = IsIndexExtended(leftHand) && IsMiddleGrabbing(leftHand) && IsRingGrabbing(leftHand) &&
                    IsLittleGrabbing(leftHand);
                break;
            case Handedness.Right:
                if (!HasUpdateSuccessFlag(updateSuccessFlags, XRHandSubsystem.UpdateSuccessFlags.RightHandJoints))
                    return;

                var rightHand = handSubsystem.rightHand;
                isPoking = IsIndexExtended(rightHand) && IsMiddleGrabbing(rightHand) && IsRingGrabbing(rightHand) &&
                    IsLittleGrabbing(rightHand);
                break;
        }

        if (isPoking && !wasPoking)
            ShowTeleportIndicator();
        else if (!isPoking && wasPoking)
            HideTeleportIndicator();
    }

    private void OnHandTrackingLost(XRHand hand)
    {
        if (isPoking && hand.handedness == handedness)
            HideTeleportIndicator();
    }

    private static bool HasUpdateSuccessFlag(XRHandSubsystem.UpdateSuccessFlags successFlags, XRHandSubsystem.UpdateSuccessFlags successFlag)
    {
        return (successFlags & successFlag) == successFlag;
    }

    private static bool IsIndexExtended(XRHand hand)
    {
        if (!(hand.GetJoint(XRHandJointID.Wrist).TryGetPose(out var wristPose) &&
                hand.GetJoint(XRHandJointID.IndexTip).TryGetPose(out var tipPose) &&
                hand.GetJoint(XRHandJointID.IndexIntermediate).TryGetPose(out var intermediatePose)))
        {
            return false;
        }

        var wristToTip = tipPose.position - wristPose.position;
        var wristToIntermediate = intermediatePose.position - wristPose.position;
        return wristToTip.sqrMagnitude > wristToIntermediate.sqrMagnitude;
    }

    private static bool IsMiddleGrabbing(XRHand hand)
    {
        if (!(hand.GetJoint(XRHandJointID.Wrist).TryGetPose(out var wristPose) &&
                hand.GetJoint(XRHandJointID.MiddleTip).TryGetPose(out var tipPose) &&
                hand.GetJoint(XRHandJointID.MiddleProximal).TryGetPose(out var proximalPose)))
        {
            return false;
        }

        var wristToTip = tipPose.position - wristPose.position;
        var wristToProximal = proximalPose.position - wristPose.position;
        return wristToProximal.sqrMagnitude >= wristToTip.sqrMagnitude;
    }

    private static bool IsRingGrabbing(XRHand hand)
    {
        if (!(hand.GetJoint(XRHandJointID.Wrist).TryGetPose(out var wristPose) &&
                hand.GetJoint(XRHandJointID.RingTip).TryGetPose(out var tipPose) &&
                hand.GetJoint(XRHandJointID.RingProximal).TryGetPose(out var proximalPose)))
        {
            return false;
        }

        var wristToTip = tipPose.position - wristPose.position;
        var wristToProximal = proximalPose.position - wristPose.position;
        return wristToProximal.sqrMagnitude >= wristToTip.sqrMagnitude;
    }

    private static bool IsLittleGrabbing(XRHand hand)
    {
        if (!(hand.GetJoint(XRHandJointID.Wrist).TryGetPose(out var wristPose) &&
                hand.GetJoint(XRHandJointID.LittleTip).TryGetPose(out var tipPose) &&
                hand.GetJoint(XRHandJointID.LittleProximal).TryGetPose(out var proximalPose)))
        {
            return false;
        }

        var wristToTip = tipPose.position - wristPose.position;
        var wristToProximal = proximalPose.position - wristPose.position;
        return wristToProximal.sqrMagnitude >= wristToTip.sqrMagnitude;
    }

    private void ShowTeleportIndicator()
    {
        isPoking = true;
        showTeleport.Invoke();
    }

    private void HideTeleportIndicator()
    {
        isPoking = false;
        hideTeleport.Invoke();
    }
}
