
/*class newsletter {*/

    //HTTP method: POST
    function createSubscriber(user) {

        const url = new URL(
            "https://api.sender.net/v2/subscribers"
        );

        let headers = {
            "Authorization": "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiM2U3ZDEwYWYyODBiNzdmODNjMjU3ODM0MTg2MzU1NzFhYjM5ZjhlNjNhYTIwNDI3YTQxZjJiNDIwMmQyOGJkMjUzZjAzMzg4YTkzMGVhNTciLCJpYXQiOiIxNjI0OTU3NDM4Ljg3NDI3NSIsIm5iZiI6IjE2MjQ5NTc0MzguODc0Mjg1IiwiZXhwIjoiNDc3ODU2MTAzOC44NzA3MzEiLCJzdWIiOiI5OTMyMiIsInNjb3BlcyI6W119.GuSdPmcEkO-OxTnAPf8rIQfT-3_CFAorfs1Oqk0kKucAIuXwQFOg7WNH7s7pfqCHe8KOuiTSntDSu2J_tLNpWg",
            "Content-Type": "application/json",
            "Accept": "application/json",
        };

        let data = {
            "email": user.Email, //The value must be a valid email address
            /*"firstname": user.FName, //Subscriber firstname
            "lastname": user.LName, //Subscriber lastname
            "groups": ["dN9gKN "], //can be one or more groups
            "fields": { "{$test_text}": "Documentation example", "{$test_num}": 8 } //Provide an array formed by key - field name, value - field value*/
        };

        fetch(url, {
            method: "POST",
            headers, body: JSON.stringify(data)
        }).then(response => response.json());

    }

    //HTTP method: DELETE
    function deleteSubscriber() {

        const url = new URL(
            "https://api.sender.net/v2/subscribers"
        );

        let headers = {
            "Authorization": "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiM2U3ZDEwYWYyODBiNzdmODNjMjU3ODM0MTg2MzU1NzFhYjM5ZjhlNjNhYTIwNDI3YTQxZjJiNDIwMmQyOGJkMjUzZjAzMzg4YTkzMGVhNTciLCJpYXQiOiIxNjI0OTU3NDM4Ljg3NDI3NSIsIm5iZiI6IjE2MjQ5NTc0MzguODc0Mjg1IiwiZXhwIjoiNDc3ODU2MTAzOC44NzA3MzEiLCJzdWIiOiI5OTMyMiIsInNjb3BlcyI6W119.GuSdPmcEkO-OxTnAPf8rIQfT-3_CFAorfs1Oqk0kKucAIuXwQFOg7WNH7s7pfqCHe8KOuiTSntDSu2J_tLNpWg",
            "Content-Type": "application/json",
            "Accept": "application/json",
        };

        let data = {
            "subscribers": ["1woqLq0", "Z6Ky1y2"]
        };

        fetch(url, {
            method: "DELETE",
            headers, body: JSON.stringify(data)
        }).then(response => response.json());

    }

    function getSubscriber(userEmail) {

        const url = new URL(
            "https://api.sender.net/v2/subscribers/by_email/{" + userEmail + "}"
        );

        let headers = {
            "Authorization": "Bearer [your-token]",
            "Content-Type": "application/json",
            "Accept": "application/json",
        };

        fetch(url, {
            method: "GET",
            headers,
        }).then(response => response.json());


    }




/*}*/
