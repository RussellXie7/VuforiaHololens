using UnityEngine;

namespace Vuforia
{
    public class PwcTarget : MonoBehaviour,
                             ITrackableEventHandler
    {
        public Fade[] fadeManagers;
        public GameObject[] UI_Elements;

        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;


        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



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

        void OnSelect()
        {
            foreach (Fade fade_m in fadeManagers)
            {
                Debug.Log("Wierd!!!!!!!!!!!!!!");
                fade_m.gameObject.SendMessageUpwards("EnableHologram");
                foreach (GameObject obj in UI_Elements)
                {
                    obj.SetActive(false);
                }
            }
        }

        void CursorHover()
        {
            foreach (GameObject obj in UI_Elements)
            {
                if (obj.activeSelf)
                    obj.SendMessageUpwards("OnHover");
            }
        }

        void CursorExit()
        {
            foreach (GameObject obj in UI_Elements)
            {
                if (obj.activeSelf)
                    obj.SendMessageUpwards("OnExit");
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            //Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            //Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            //// Enable rendering:
            //foreach (Renderer component in rendererComponents)
            //{
            //    component.enabled = true;
            //}


            // do Fade
            foreach (Fade fade_m in fadeManagers) fade_m.SendMessage("OnTrack");

            // Enable colliders:
            //foreach (Collider component in colliderComponents)
            //{
            //    component.enabled = true;
            //}

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            //Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            //Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);


            //// Do Fade
            //Fade[] fadeManagers = GetComponentsInChildren<Fade>();

            foreach (Fade fade_m in fadeManagers) fade_m.SendMessage("LostTrack");


            //// Disable colliders:
            //foreach (Collider component in colliderComponents)
            //{
            //    component.enabled = false;
            //}

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");


        }

        #endregion // PRIVATE_METHODS
    }
}
