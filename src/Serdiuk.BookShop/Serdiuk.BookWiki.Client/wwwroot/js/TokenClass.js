class TokenClass {

    static async adminCheck() {
        let canUse = await this.canUseToken()
        if (!canUse) return false;

        const token = localStorage.getItem('access');

        const decodedToken = JSON.parse(atob(token.split('.')[1]));
        const roles = decodedToken.role;
        if (!roles) return false;
        return roles.includes('Administrator');
    }

    static updateTokens(access, refresh) {
        localStorage.removeItem('access');
        localStorage.removeItem('refresh');

        localStorage.setItem('access', access);
        localStorage.setItem('refresh', refresh);
    }

    static async canUseToken() {
        const token = localStorage.getItem('access');
        if (!token) return false;

        const decodedToken = JSON.parse(atob(token.split('.')[1]));
        const expirationTime = decodedToken.exp;
        const currentTime = new Date().getTime() / 1000;

        if (currentTime >= expirationTime) {
            return TokenClass.tryRefreshToken(); 
        }
        else {
            return true;
        }
    }

    static async tryRefreshToken() {
        const AUTH_URL = 'https://localhost:7123/api/account';

        const refresh = localStorage.getItem('refresh');
        const access = localStorage.getItem('access');
        if (!refresh) return false;

        const url = `${AUTH_URL}/Refresh`;
        const data = {
            accessToken: access,
            refreshToken: refresh
        };

        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });

            const result = await response.json();
            console.log(result);
            if (!result.result) {
                localStorage.removeItem('access');
                localStorage.removeItem('refresh');
            }
            else {
                TokenClass.updateTokens(result.access, result.refresh);
            }
            return result.result;
        } catch (error) {
            localStorage.removeItem('access');
            localStorage.removeItem('refresh');
            console.error('An error occurred:', error);
            return false;
        }
    }
    static async logOut() {
        if (confirm("Are your sure want logout?")) {
            this.updateTokens("", "");
            location.reload();
        }
    }
}
