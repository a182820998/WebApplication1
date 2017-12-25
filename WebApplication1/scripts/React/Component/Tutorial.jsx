var CommentBox = React.createClass({
    render() {
        return (
            <div>
                Hello, world! I am a CommentBox.
            </div>
        );
    }
});

React.render(
    <CommentBox/>,
    document.getElementById('content')
);