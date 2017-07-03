using UnityEngine;

namespace Vuforia
{
    public class PwcTarget : MonoBehaviour,
                             ITrackableEventHandler
    {

        public GameObject dataBoard;
        public GameObject orangeTarget;
        public GameObject coffeeTarget;

        [HideInInspector]
        public bool found = false;

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

        void CursorHover()
        {
            if (dataBoard.activeSelf)
                dataBoard.SendMessageUpwards("OnHover");
        }

        void CursorExit()
        {

            if (dataBoard.activeSelf)
                dataBoard.SendMessageUpwards("OnExit");

        }

        void OnSelect()
        {
            dataBoard.SendMessageUpwards("FixPosition");
            dataBoard.SendMessageUpwards("EnableCollider");


            orangeTarget.SetActive(true);
            coffeeTarget.SetActive(true);
            gameObject.SetActive(false);

        }

        void CheckFound()
        {
            if (found)
            {
                EnableCollider();
            }
            else
            {
                DisableCollider();
            }
        }

        public void EnableCollider()
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        public void DisableCollider()
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
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
            //foreach (GameObject info_element in info_elements) info_element.SendMessageUpwards("OnTrack");

            // Enable colliders:
            //foreach (Collider component in colliderComponents)
            //{
            //    component.enabled = true;
            //}

            EnableCollider();
            found = true;
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            //Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            //Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);


            //// Do Fade
            //Fade[] fadeManagers = GetComponentsInChildren<Fade>();

            //foreach (GameObject info_element in info_elements) info_element.SendMessageUpwards("LostTrack");


            //// Disable colliders:
            //foreach (Collider component in colliderComponents)
            //{
            //    component.enabled = false;
            //}
            DisableCollider();
            found = false;
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");


        }

        private void OnEnable()
        {
            found = false;
        }


        #endregion // PRIVATE_METHODS
    }
}
