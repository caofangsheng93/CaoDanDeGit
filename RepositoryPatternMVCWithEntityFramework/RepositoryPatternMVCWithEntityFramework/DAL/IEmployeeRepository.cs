using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternMVCWithEntityFramework.DAL
{
    interface IEmployeeRepository<T> where T:class
    {
        /// <summary>
        /// 获取所有的Employee
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAllEmployee();

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetEmployeeById(object id);

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="obj"></param>
        void InsertEmployee(T obj);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="obj"></param>
        void UpdateEmployee(T obj);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        void DeleteEmployee(object id);

        /// <summary>
        /// 保存
        /// </summary>
        void Save();


        /// <summary>
        /// 清理资源
        /// </summary>
        void Dispose();


    }
}
