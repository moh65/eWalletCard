
export default (function AuthKey() {
    let _key = "";
    
    return {
        get: () => _key,
        set: (value) => {
            _key = value;
        }
    }
})();