var CommentBox = React.createClass({
    getInitialState: function() {
        return { data: [] };
    },

    componentWillMount: function() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function() {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        }.bind(this);

        xhr.send();
    },
    
    render: function() {
        return (
            <div>
                <h1>Infomation</h1>
                <CommentList data={this.state.data} />
                <br/>
            </div>
        );
    }
});

var CommentList = React.createClass({
    render: function () {
        var commentNodes = this.props.data.map(function(model) {
            return (
                <Comment student={model.Name }>
                    {model.Math}
                </Comment>
            );
        });

        return (
            <div>
                {commentNodes}
            </div>
        );
    }
});

var Comment = React.createClass({
    getInitialState: function () {
        return {
            isSelected: false
        };
    },

    handleClick: function () {
        this.setState({
            isSelected: true
        });
    },

    render: function () {
        var isSelected = this.state.isSelected;
        var style = {
            'background-color': ''
        };

        if (isSelected) {
            style = {
                'background-color': '#ccc'
            };
        }

        return (
            <div>
                <h2 onClick={this.handleClick} style={style}>
                    {this.props.student}
                </h2>
                {this.props.children}
            </div>
        );
    }
});

React.render(
    <CommentBox url="/React/ReactJson"/>,
    document.getElementById('content')
);