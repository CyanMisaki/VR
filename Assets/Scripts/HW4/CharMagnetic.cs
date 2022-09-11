using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace HW4
{

    [System.Serializable]
    public struct MagneticPoint
    {
        public List<SpringJoint> JointList;
        public List<Rigidbody> RG;
        public List<ParticleSystem> HighLight;
        public Transform BlueObj, RedObj;
        public Vector3 BluePos, RedPos;
    }
    public class CharMagnetic : MonoBehaviour
    {
        [SerializeField] private float SpellDistance = 20;
        [SerializeField] private float MaxMagniteForce = 50;
        [SerializeField] private MagneticPoint MagniteSpell;
        [SerializeField] private Transform BlueHolder, RedHolder;
        [SerializeField] private Material RedMat, BlueMat, YellowMat;
        [SerializeField] private ParticleSystem hl_Reference;

        public void SetBlue(Transform trans)
        {
            MagniteSpell.BlueObj = trans;
            MagniteSpell.BluePos = trans.position;
            Highlighting(true, trans);
            CheckToJoint();
        }
        
        public void SetBlue(Vector3 trans)
        {
            MagniteSpell.BlueObj = BlueHolder;
            MagniteSpell.BluePos = trans;
            BlueHolder.position = trans;
            BlueHolder.GetChild(0).gameObject.SetActive(true);
            CheckToJoint();
        }
        
        public void SetRed(Transform trans)
        {
            MagniteSpell.RedObj = trans;
            MagniteSpell.RedPos = trans.position;
            Highlighting(false, trans);
            CheckToJoint();
        }

       public void SetRed(Vector3 trans)
        {
            MagniteSpell.RedObj = RedHolder;
            MagniteSpell.RedPos = trans;
            RedHolder.position = trans;
            RedHolder.GetChild(0).gameObject.SetActive(true);
            CheckToJoint();
        }

        private void CheckToJoint()
        {
            if (MagniteSpell.BlueObj == null || MagniteSpell.RedObj == null) return;
            
            if (Vector3.Distance(MagniteSpell.RedPos, MagniteSpell.BluePos) < SpellDistance)
                CreateJoint();
            else EreaseSpell();
        }

        private void CreateJoint()
        {
            var sp = MagniteSpell.BlueObj.gameObject.AddComponent<SpringJoint>();
            sp.autoConfigureConnectedAnchor = false;
            sp.anchor = Vector3.zero;
            sp.connectedAnchor = Vector3.zero;
            sp.enableCollision = true;
            sp.enablePreprocessing = false;

            sp.connectedBody = MagniteSpell.RedObj.GetComponent<Rigidbody>();

            EreaseSpell();
            MagniteSpell.JointList.Add(sp);

            var rg = sp.GetComponent<Rigidbody>();
            MagniteSpell.RG.Add(rg);

            AddRG(sp.connectedBody);
        }

        private void AddRG(Rigidbody RG)
        {
            if(MagniteSpell.RG==null) return;

            for (var i = 0; i < MagniteSpell.RG.Count; i++)
            {
                if(RG==MagniteSpell.RG[i]) break;
                if (i != MagniteSpell.RG.Count - 1) continue;
                
                MagniteSpell.RG.Add(RG);
                break;

            }
        }
        
        private void Highlighting(bool IsBlue, Transform trans)
        {
            var ps = Instantiate(hl_Reference, trans, false);
            ps.GetComponent<Renderer>().material = IsBlue ? BlueMat : RedMat;
            
            MagniteSpell.HighLight.Add(ps);
        }
        
        private void EreaseSpell()
        {
            MagniteSpell.BlueObj = null;
            MagniteSpell.RedObj = null;

            foreach (var t in MagniteSpell.HighLight)
                t.GetComponent<Renderer>().material = YellowMat;
        }

        public void DestroyAllJoints()
        {
            foreach (var t in MagniteSpell.JointList)
                Destroy(t);
            
            foreach (var t in MagniteSpell.RG)
            {
                t.angularDrag = 0.05f;
                t.drag = 0;
                t.WakeUp();
            }
            
            MagniteSpell.JointList.Clear();
            MagniteSpell.RG.Clear();
            EreaseSpell();
            
            foreach (var t in MagniteSpell.HighLight)
                Destroy(t);
            
            MagniteSpell.HighLight.Clear();
            DisableHolders();
        }
        private void DisableHolders()
        {
            BlueHolder.GetChild(0).gameObject.SetActive(false);
            RedHolder.GetChild(0).gameObject.SetActive(false);
        }

        public void ChangeSpringPower(float fNum)
        {
            if (MagniteSpell.JointList.Count <= 0) return;
            
            foreach (var sj in MagniteSpell.JointList)
            {
                sj.spring += fNum;
                sj.damper += fNum;

                sj.damper += Mathf.Clamp(sj.damper, 0, MaxMagniteForce);
                sj.spring += Mathf.Clamp(sj.spring, 0, MaxMagniteForce);
            }

            foreach (var rg in MagniteSpell.RG)
            {
                rg.WakeUp();
                rg.angularDrag += fNum;
                rg.drag += fNum;

                rg.angularDrag = Mathf.Clamp(rg.angularDrag, 0, MaxMagniteForce);
                rg.drag = Mathf.Clamp(rg.drag, 0, MaxMagniteForce);
            }
        }
    }
}