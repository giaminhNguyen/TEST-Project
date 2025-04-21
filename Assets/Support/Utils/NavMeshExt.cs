using UnityEngine;
using UnityEngine.AI;

namespace UltimateHelper
{
    /// <summary>
    /// Lớp chứa các phương thức mở rộng và tiện ích cho NavMesh.
    /// </summary>
    public static class NavMeshExt
    {
        #if USE_NAVMESH
        /// <summary>
        /// Kiểm tra xem một điểm có nằm trên NavMesh hay không.
        /// </summary>
        /// <param name="position">Vị trí cần kiểm tra.</param>
        /// <param name="distanceThreshold">Khoảng cách tối đa để xác định điểm nằm trên NavMesh.</param>
        /// <returns>Trả về true nếu điểm nằm trên NavMesh, ngược lại là false.</returns>
        public static bool IsPointOnNavMesh(Vector3 position, float distanceThreshold = 1.0f)
        {
            return NavMesh.SamplePosition(position, out var hit, distanceThreshold, NavMesh.AllAreas);
        }
        
        /// <summary>
        /// Kiểm tra xem NavMeshAgent có hợp lệ và đang nằm trên NavMesh hay không.
        /// </summary>
        /// <param name="agent">Đối tượng NavMeshAgent cần kiểm tra.</param>
        /// <returns>
        /// True nếu NavMeshAgent đang hoạt động, được kích hoạt và nằm trên NavMesh. 
        /// Ngược lại, trả về False.
        /// </returns>
        public static bool IsAgentValid(this NavMeshAgent agent)
        {
            return agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh;
        }
        
        /// <summary>
        /// Dừng hoặc tiếp tục di chuyển của NavMeshAgent.
        /// </summary>
        /// <param name="agent">Đối tượng NavMeshAgent cần kiểm tra.</param>
        /// <param name="isStop">True để dừng, False để tiếp tục di chuyển.</param>
        public static void SetStop(this NavMeshAgent agent,bool isStop)
        {
            if (!IsAgentValid(agent)) return;
            agent.isStopped = isStop;
        }
        
        /// <summary>
        /// Tìm đường đi ngắn nhất và di chuyển từ vị trí này đến vị trí khác trên NavMesh.
        /// </summary>
        /// <param name="agent">NavMeshAgent</param>
        /// <param name="destination">Điểm đến</param>
        public static bool MoveShortestPath(this NavMeshAgent agent, Vector3 destination)
        {
            if (!IsAgentValid(agent)) return false;
            return agent.SetDestination(destination);
        }
        
        /// <summary>
        /// Dừng ngay lập tức NavMeshAgent.
        /// </summary>
        /// <param name="agent">Đối tượng NavMeshAgent cần dừng.</param>
        public static void StopImmediately(this NavMeshAgent agent)
        {
            if (!IsAgentValid(agent) && !agent.IsMoving()) return;
            agent.ResetPath();
            agent.velocity = Vector3.zero;
        }
        
        /// <summary>
        /// Kiểm tra xem vị trí có nằm trên NavMesh không.
        /// </summary>
        /// <param name="position">Vị trí</param>
        /// <returns>True nếu nằm trên NavMesh, False nếu không</returns>
        public static bool IsOnNavMesh(Vector3 position)
        {
            return NavMesh.SamplePosition(position, out var hit, 0.5f, NavMesh.AllAreas);
        }
        
        /// <summary>
        /// Kiểm tra xem agent có đang di chuyển không.
        /// </summary>
        /// <param name="agent">NavMeshAgent</param>
        /// <returns>True nếu đang di chuyển, False nếu không</returns>
        public static bool IsMoving(this NavMeshAgent agent)
        {
            return agent.velocity.magnitude > 0.1f;
        }

        /// <summary>
        /// Tìm điểm gần nhất trên NavMesh từ một vị trí cụ thể.
        /// </summary>
        /// <param name="position">Vị trí gốc.</param>
        /// <param name="maxDistance">Khoảng cách tìm kiếm tối đa.</param>
        /// <returns>Trả về vị trí trên NavMesh gần nhất hoặc vị trí gốc nếu không tìm thấy.</returns>
        public static Vector3 GetNearestPointOnNavMesh(Vector3 position, float maxDistance = 1.0f)
        {
            if (NavMesh.SamplePosition(position, out var hit, maxDistance, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return position;
        }

        /// <summary>
        /// Di chuyển NavMeshAgent đến một vị trí mới và đảm bảo nó nằm trên NavMesh.
        /// </summary>
        /// <param name="agent">NavMeshAgent cần di chuyển.</param>
        /// <param name="newPosition">Vị trí mới.</param>
        /// <returns>Trả về true nếu di chuyển thành công, ngược lại là false.</returns>
        public static bool WarpAgentToPosition(NavMeshAgent agent, Vector3 newPosition)
        {
            Vector3 nearestNavMeshPosition = GetNearestPointOnNavMesh(newPosition);

            if (IsPointOnNavMesh(nearestNavMeshPosition))
            {
                return agent.Warp(nearestNavMeshPosition);
            }

            return false;
        }
        
        /// <summary>
        /// Đặt agent về vị trí gần nhất trên NavMesh.
        /// </summary>
        /// <param name="agent">NavMeshAgent</param>
        public static void SnapToNavMesh(this NavMeshAgent agent)
        {
            if (NavMesh.SamplePosition(agent.transform.position, out var hit,Mathf.Infinity, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
            }
        }

        /// <summary>
        /// Tính toán đường đi từ NavMeshAgent đến một vị trí đích.
        /// </summary>
        /// <param name="agent">NavMeshAgent.</param>
        /// <param name="destination">Vị trí đích.</param>
        /// <returns>Trả về true nếu tìm thấy đường đi hợp lệ, ngược lại là false.</returns>
        public static bool CalculatePathToDestination(NavMeshAgent agent, Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();

            if (agent.CalculatePath(destination, path) && path.status == NavMeshPathStatus.PathComplete)
            {
                agent.SetPath(path);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Kiểm tra xem NavMeshAgent có thể đến được vị trí đích hay không.
        /// </summary>
        /// <param name="agent">NavMeshAgent.</param>
        /// <param name="destination">Vị trí đích.</param>
        /// <returns>Trả về true nếu có thể đến được, ngược lại là false.</returns>
        public static bool CanAgentReachDestination(NavMeshAgent agent, Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();

            if (agent.CalculatePath(destination, path))
            {
                return path.status == NavMeshPathStatus.PathComplete;
            }

            return false;
        }
        #endif
    }
}