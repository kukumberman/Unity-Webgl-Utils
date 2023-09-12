# Unity-Webgl-Utils

### Description

Few utilities which I usually use when exporting to **WebGL**

- #### Anti-piracy measures
Prevents from downloading build files and publishing it elsewhere by unfair person.

You need to configure properties on which websites game will run.

- #### Country blocker
Prevents game from running on someone's device if its country has been blacklisted by the author.

⚠️ However the access will be granted if person selects other country via VPN. 

Relies on external website by checking IP address https://api.country.is/

Similar functionality has http://ip-api.com/json but unavailable in WebGL build due to browser restrictions.

### Installation

Install via **Package Manager** from git URL.
