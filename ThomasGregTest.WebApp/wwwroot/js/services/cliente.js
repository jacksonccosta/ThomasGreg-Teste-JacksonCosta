class Cliente {
    static url = 'cliente';

    static deleteAxios = async (id) => {
        await API.authBearer();
        return await axios.delete(`${API.urlApi}${this.url}/${id}`);
    }
}